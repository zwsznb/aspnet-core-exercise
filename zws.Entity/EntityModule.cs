using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using zws.Core.Abstract;
using zws.Test1;

namespace zws.Entity
{
    [CustomDependence(typeof(Test1Module))]
    public class EntityModule : IModule
    {
        public void ApplicationConfig(WebApplication application)
        {
            Console.WriteLine("EntityModule ApplicationConfig加载");
        }

        public void ConfigService(IServiceCollection service)
        {
            Console.WriteLine("EntityModule ConfigService加载");
        }
    }
}