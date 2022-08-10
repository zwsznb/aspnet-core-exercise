using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zws.Core.Abstract;
using zws.Core.Entity;

namespace zws.Core.Instructure
{
    internal class UserRespository : IUserRespository
    {
        public User GetUser()
        {
            return new User { Id = 1, Name = "zws" };
        }
    }
}
