using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using zws.Core.Abstract;
using zws.Core.Common.Mvc;

namespace zws.Core.Extensions
{
    public static class ApplicationExtension
    {
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

        public static void AddModule<T>(this WebApplication app) where T : IModule
        {
            var list = new List<Type>();
            Rescuise(typeof(T), list);
            //去重，然后加载配置
            var tempList = new List<Type>();
            foreach (var t in list)
            {
                if (!tempList.Contains(t))
                {
                    tempList.Add(t);
                }
            }

            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var serviceCollection = services.GetRequiredService<IServiceCollection>();
                execModule(tempList, app, serviceCollection);
            }
        }

        private static void execModule(List<Type> tempList, WebApplication app, IServiceCollection serviceCollection)
        {
            foreach (var module in tempList)
            {
                var mod = (IModule)Activator.CreateInstance(module);
                mod.ConfigService(serviceCollection);
            }
            foreach (var module in tempList)
            {
                var mod = (IModule)Activator.CreateInstance(module);
                mod.ApplicationConfig(app);
            }
        }

        public static void Rescuise(Type type, List<Type> list)
        {
            var attr = type.GetCustomAttributes(false);
            if (attr.Length == 0)
            {
                list.Add(type);
                return;
            }
            foreach (var a in attr)
            {
                var temp = (CustomDependenceAttribute)a;
                Rescuise(temp.DependType, list);
            }
            list.Add(type);
        }
    }
}
