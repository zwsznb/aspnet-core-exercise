using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
