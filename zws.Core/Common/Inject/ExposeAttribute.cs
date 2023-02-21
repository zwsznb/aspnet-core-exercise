namespace zws.Core.Common.Inject
{
    public class ExposeAttribute : Attribute
    {
        public ExposeAttribute(Type _type)
        {
            type = _type;
        }
        public Type type { get; set; }

    }
}
