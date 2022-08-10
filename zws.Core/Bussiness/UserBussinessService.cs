using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zws.Core.Abstract;

namespace zws.Core.Bussiness
{
    public class UserBussinessService : IUserBussinessService
    {
        private readonly IUserRespository userRespository;
        public UserBussinessService(IUserRespository respository)
        {
            userRespository = respository;
        }
        public void CallUser()
        {
            var user = userRespository.GetUser();
            Console.WriteLine(user.ToString());
        }
    }
}
