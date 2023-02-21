using zws.Application;
using zws.Core.Abstract;
using zws.Entity;
using zws.Test1;

namespace WebApplication2
{
    [CustomDependence(typeof(EntityModule))]
    [CustomDependence(typeof(ApplicationServiceModule))]
    public class MainModule : IModule
    {
        public void ApplicationConfig(WebApplication application)
        {
            Console.WriteLine("MainModule ApplicationConfig加载");
        }

        public void ConfigService(IServiceCollection service)
        {
            Console.WriteLine("MainModule ConfigService加载");
        }
    }
}
