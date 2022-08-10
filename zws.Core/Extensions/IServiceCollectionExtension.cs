using Microsoft.Extensions.DependencyInjection;
using zws.Core.Common.Mvc;

namespace zws.Core.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddCustomConvention(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Conventions.Add(new CustomApplicationModelConvention());
            });
        }
    }
}
