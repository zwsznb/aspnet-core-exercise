using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using zws.Core.Abstract;

namespace zws.Test2
{
    public class Test2Module : IModule
    {
        public void ApplicationConfig(WebApplication application)
        {
            Console.WriteLine("Test2Module ApplicationConfig加载");
        }

        public void ConfigService(IServiceCollection service)
        {
            Console.WriteLine("Test2Module ConfigService加载");
        }
    }
}