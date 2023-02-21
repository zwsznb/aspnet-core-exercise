using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using zws.Core.Abstract;
using zws.Test2;

namespace zws.Application
{
    [CustomDependence(typeof(Test2Module))]
    public class ApplicationServiceModule : IModule
    {
        public void ApplicationConfig(WebApplication application)
        {
            Console.WriteLine("ApplicationServiceModule ApplicationConfig加载");
        }

        public void ConfigService(IServiceCollection service)
        {
            Console.WriteLine("ApplicationServiceModule ServiceConfig加载");
        }
    }
}
