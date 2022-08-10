using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using zws.Core.Abstract.Mvc;

namespace zws.Core.Common.Mvc
{
    /// <summary>
    /// 用来识别控制器
    /// </summary>
    public class CustomControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblies = assembly.GetReferencedAssemblies();
            assemblies.Append<AssemblyName>(assembly.GetName());
            assemblies.ToList().ForEach(assembly =>
            {
                var assTemp = Assembly.Load(assembly);
                assTemp.GetTypes().ToList().ForEach(type =>
                {
                    //如果实现了IRomteService则认为是一个控制器
                    if (!IsController(type))
                    {
                        return;
                    }
                    feature.Controllers.Add(type.GetTypeInfo());
                });
            });
        }
        public bool IsController(Type type)
        {
            if (!type.IsClass)
            {
                return false;
            }

            if (type.IsAbstract)
            {
                return false;
            }
            if (!type.IsPublic)
            {
                return false;
            }

            if (type.ContainsGenericParameters)
            {
                return false;
            }

            if (type.IsAssignableTo(typeof(IRomteService)))
            {
                return true;
            }
            return false;
        }
    }
}
