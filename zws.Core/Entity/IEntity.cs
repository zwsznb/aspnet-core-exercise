using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zws.Core.Entity
{
    internal interface IEntity<T>
    {
        T Id { get; set; }
    }
}
