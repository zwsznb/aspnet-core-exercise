namespace zws.Core.Common.Inject
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
