using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using zws.Core.Abstract;
using zws.Test2;

namespace zws.Test1
{
    public class Test1Module : IModule
    {
        public void ApplicationConfig(WebApplication application)
        {
            Console.WriteLine("Test1Module ApplicationConfig加载");
        }

        public void ConfigService(IServiceCollection service)
        {
            Console.WriteLine("Test1Module ConfigService加载");
        }
    }
}