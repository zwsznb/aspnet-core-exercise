using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zws.Core.Abstract;

namespace zws.Core.Common
{
    public static class InjectTypeChecker
    {
        private static InjectTypeContext context = new InjectTypeContext();
        public static bool IsImplementInjectType(Type[] types)
        {
            foreach (var t in types)
            {
                if (context.types.Contains(t))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsImplementInjectType(Type type)
        {
            return context.types.Contains(type);
        }

        public static bool IsImplementInjectType(Type[] types, out Type type)
        {
            type = null;
            foreach (var t in types)
            {
                if (context.types.Contains(t))
                {
                    type = t;
                    return true;
                }
            }
            return false;
        }
    }
}
