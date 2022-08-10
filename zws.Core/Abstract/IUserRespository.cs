using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zws.Core.Common;
using zws.Core.Entity;

namespace zws.Core.Abstract
{
    public interface IUserRespository : IRespository
    {
        User GetUser();
    }
}
