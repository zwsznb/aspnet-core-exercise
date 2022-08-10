using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using zws.Core.Abstract;
using zws.Core.Extensions;

namespace zws.Core.Common.Mvc
{
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
            //实际上路由凭借还是自己填补RouteAttribute特性
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
            return reqM.Last()._method.ToString();
        }

        private void ConfigureApiExplorer(ControllerModel controller)
        {
            //将自动发现的控制器显示出来
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
}
