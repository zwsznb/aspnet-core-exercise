using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zws.Core.Abstract;

namespace zws.Core.Common
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
