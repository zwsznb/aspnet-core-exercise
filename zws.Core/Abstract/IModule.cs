using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace zws.Core.Abstract
{
    public interface IModule
    {
        void ConfigService(IServiceCollection service);
        void ApplicationConfig(WebApplication application);
    }
}
