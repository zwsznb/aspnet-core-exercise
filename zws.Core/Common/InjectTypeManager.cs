using Microsoft.Extensions.DependencyInjection;
using zws.Core.Abstract;

namespace zws.Core.Common
{
    public static class InjectTypeManager
    {
        public static void Inject(IServiceCollection service, Type interfaceType, Type implementType, Type injectType)
        {
            if (injectType.FullName.Equals(typeof(ISingleton).FullName))
            {
                AddISingleton(service, interfaceType, implementType);
            }
            if (injectType.FullName.Equals(typeof(IScope).FullName))
            {
                AddIScope(service, interfaceType, implementType);
            }
            if (injectType.FullName.Equals(typeof(ITransient).FullName))
            {
                AddITransient(service, interfaceType, implementType);
            }

        }
        private static void AddISingleton(IServiceCollection service, Type interfaceType, Type implementType)
        {
            ImplementTypeThrowException(service, implementType);
            if (interfaceType == null)
            {
                service.AddSingleton(implementType);
                return;
            }
            service.AddSingleton(interfaceType, implementType);
        }

        private static void ImplementTypeThrowException(IServiceCollection service, Type implementType)
        {
            if (implementType == null)
            {
                throw new ArgumentNullException(nameof(implementType));
            }
            if (service == null)
            {
                throw new Exception("ServiceCollection is null,this fail is occur in coustom inject.");
            }
        }

        private static void AddIScope(IServiceCollection service, Type interfaceType, Type implementType)
        {
            ImplementTypeThrowException(service, implementType);
            if (interfaceType == null)
            {
                service.AddScoped(implementType);
                return;
            }
            service.AddScoped(interfaceType, implementType);
        }
        private static void AddITransient(IServiceCollection service, Type interfaceType, Type implementType)
        {
            ImplementTypeThrowException(service, implementType);
            if (interfaceType == null)
            {
                service.AddTransient(implementType);
                return;
            }
            service.AddTransient(interfaceType, implementType);
        }
    }
}
