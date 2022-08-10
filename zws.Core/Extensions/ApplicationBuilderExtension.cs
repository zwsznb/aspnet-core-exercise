using System.Reflection;
using zws.Core.Common;
using Microsoft.AspNetCore.Builder;

namespace WebApplication2.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void AddCustomInject(this WebApplicationBuilder builder)
        {
            builder.AddInterfaceInject();
        }
        private static List<AssemblyName> GetReferenceCustomAssembliesName(Assembly assembly)
        {
            var assembliesName = new List<AssemblyName>();
            assembliesName.Add(assembly.GetName());
            assembliesName.AddRange(assembly.GetReferencedAssemblies().ToList());
            return assembliesName;
        }


        public static void AddInterfaceInject(this WebApplicationBuilder builder)
        {
            builder.AddInterfaceInject(Assembly.GetEntryAssembly());
        }
        public static void AddInterfaceInject(this WebApplicationBuilder builder, Assembly assembly)
        {
            //最简单的实现就是实现了接口就注入
            AssemblyIsNullThrowException(assembly);
            var CustomAssembliesName = GetReferenceCustomAssembliesName(assembly);
            CustomAssembliesName.ForEach(a =>
            {
                var ass = Assembly.Load(a);
                var type = typeof(object);
                ass.GetTypes().ToList().ForEach(t =>
                {
                    if (t.IsClass && InjectTypeChecker.IsImplementInjectType(t.GetInterfaces(), out type))
                    {
                        InjectInContainer(builder, t, type);
                    }
                });
            });
        }

        private static void InjectInContainer(WebApplicationBuilder builder, Type ImplementType, Type injectType)
        {
            var attr = ImplementType.GetCustomAttribute<ExposeAttribute>();
            var firstInterface = ImplementType.GetInterfaces()[0];
            var services = builder.Services;
            //如果没有使用暴露属性，第一个接口为注入接口，直接注入实现注入接口类
            if (attr == null && InjectTypeChecker.IsImplementInjectType(firstInterface))
            {
                InjectTypeManager.Inject(services, null, ImplementType, injectType);
            }
            //如果没有使用暴露属性，并且第一个接口不为注入接口，直接使用第一个接口作为对外接口
            if (attr == null && !InjectTypeChecker.IsImplementInjectType(firstInterface))
            {
                InjectTypeManager.Inject(services, firstInterface, ImplementType, injectType);
            }
            //如果使用了暴露属性，直接注入属性里面的类型
            if (attr != null)
            {
                // TODO 容错处理,比如说只实现了一个接口，且接口为注入接口怎么办？
                // 特性写了实现类型怎么办
                InjectTypeManager.Inject(services, attr.type, ImplementType, injectType);
            }
        }


        private static void AssemblyIsNullThrowException(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
        }
    }
}
