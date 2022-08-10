using zws.Core.Abstract.Inject;

namespace zws.Core.Common.Inject
{
    public class InjectTypeContext
    {
        public List<Type> types { get; }
        public InjectTypeContext()
        {
            types = new List<Type>();
            types.Add(typeof(ITransient));
            types.Add(typeof(ISingleton));
            types.Add(typeof(IScope));
        }
    }
}
