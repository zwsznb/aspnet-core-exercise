# 基于.Net6简单实现自动发现api

> 因为使用过abp，最近又看了下.Net6的源码，所以就想写个简单的例子

## 如何发现自定义Controller

> 实现自动发现api，实际上就是要构造Controller，我们平时创建的Controller一般都是在web项目中的，它们会被框架默认构造，所以我们需要找到源头。即给**ApplicationPartManager**添加一个**IApplicationFeatureProvider**，上代码

```csharp
//编写WebApplication扩展方法，获取ApplicationPartManager并将自定义的IApplicationFeatureProvider添加进去
public static void AddCustomController(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var applicationPartManager = services.GetRequiredService<ApplicationPartManager>();
                if (applicationPartManager == null)
                {
                    throw new Exception("未在容器中找到ApplicationPartManager");
                }
                applicationPartManager.FeatureProviders.Add(new CustomControllerFeatureProvider());
            }
        }
```

```csharp
public class CustomControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblies = assembly.GetReferencedAssemblies();
            assemblies.Append<AssemblyName>(assembly.GetName());
            assemblies.ToList().ForEach(assembly =>
            {
                var assTemp = Assembly.Load(assembly);
                assTemp.GetTypes().ToList().ForEach(type =>
                {
                    //如果实现了IRomteService则认为是一个控制器
                    if (!IsController(type))
                    {
                        return;
                    }
                    feature.Controllers.Add(type.GetTypeInfo());
                });
            });
        }
     }
```

> 在**CustomControllerFeatureProvider**中，获取web项目的引用集并将实现了**IRomteService**接口的类添加为**Controller**；做完之后发现，这里使用引用集的方式也是存在问题的，就是说**GetReferencedAssemblies**获取的引用集一定web项目是用到的，不然是获取不到的，可能Abp的模块依赖特性就是解决这个问题的，我没有过于纠结，毕竟目标不是这个
>
> 现在我们只要调用**AddCustomController**方法就可以看到我们想被自动发现的api了

## 构造Controller

> 我们的自定义Controller被成功添加后，通过跟框架默认的Controller进行对比，就会发现压根不完整，是不可用的，所以我们现在需要将自定义的Controller构造成可用的Controller，不求完整，这里我纠结了很久，最后还是参考了Abp的源码，直接复制过来用了，上代码

```csharp
public class CustomApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (controller.ControllerType.IsAssignableTo(typeof(IRomteService)))
                {
                    ConfigureApiExplorer(controller);
                    ConfigureSelector(controller);
                    ConfigureParameters(controller);
                }
            }
        }
        //直接抄abp代码
        private void ConfigureParameters(ControllerModel controller)
        {
            foreach (var action in controller.Actions)
            {
                foreach (var prm in action.Parameters)
                {
                    if (prm.BindingInfo != null)
                    {
                        continue;
                    }

                    if (!TypeHelper.IsPrimitiveExtended(prm.ParameterInfo.ParameterType, includeEnums: true))
                    {
                        if (CanUseFormBodyBinding(action, prm))
                        {
                            prm.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                        }
                    }
                }
            }
        }
        private bool CanUseFormBodyBinding(ActionModel action, ParameterModel parameter)
        {
            //如果参数名为id则默认为query param
            if (parameter.ParameterName == "id")
            {
                return false;
            }

            foreach (var selector in action.Selectors)
            {
                if (selector.ActionConstraints == null)
                {
                    continue;
                }

                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    var httpMethodActionConstraint = actionConstraint as HttpMethodActionConstraint;
                    if (httpMethodActionConstraint == null)
                    {
                        continue;
                    }

                    if (httpMethodActionConstraint.HttpMethods.All(hm => hm.IsIn("GET", "DELETE", "TRACE", "HEAD")))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ConfigureSelector(ControllerModel controller)
        {
            //先不使用特性，做个最简单的
            var controllerName = controller.ControllerName;
            foreach (var action in controller.Actions)
            {
                action.Selectors.Clear();
                var reqMethod = GetRequestMethod(action);
                action.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"/{controllerName}/{action.ActionName}")),
                    ActionConstraints = { new HttpMethodActionConstraint(new[] { reqMethod }) }
                });
            }
        }

        private string GetRequestMethod(ActionModel action)
        {
            var reqM = action.Attributes.OfType<RequestMethodAttribute>().ToList();
            if (reqM.Count == 0)
            {
                return RequestMethods.GET.ToString();
            }
            return reqM.Last()._method.ToString();
        }

        private void ConfigureApiExplorer(ControllerModel controller)
        {
            controller.ApiExplorer.IsVisible = true;
            foreach (var action in controller.Actions)
            {
                ConfigureApiExplorer(action);
            }
        }

        private void ConfigureApiExplorer(ActionModel action)
        {
            action.ApiExplorer.IsVisible = true;
        }
    }
```

> 实现了**IApplicationModelConvention**的类会被注入到容器，Abp中也是实现了这个接口来实现构造的，看过源码就知道，所有Controller被构造后会被放到一个上下文中，并作为参数传给所有实现了**IApplicationModelConvention**的类的Apply方法
>
> **ConfigureApiExplorer**方法只是用来设置api可不可见，一般不管也可以
>
> **ConfigureSelector**方法用于添加选择器，其实也是在给Controller或者Action添加**RouteAttribute**，我们就可以从这里进行url的配置，这里只是简单的拼接了一下
>
> **ConfigureParameters**方法用来配置请求参数，这里直接复制abp源码了，最关键的一步就是参数绑定，即绑定asp.net core系统绑定的参数，没有参数绑定的话，其实也是可用的，能接收的参数就只有url后面的查询参数了，也只适用于GET请求方式，所以参数绑定是必要的



