namespace zws.Core.Abstract
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true)]
    public class CustomDependenceAttribute : Attribute
    {
        public CustomDependenceAttribute(Type type)
        {
            this.DependType = type;
        }
        public Type DependType { get; set; }
    }
}
