using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zws.Core.Common
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
