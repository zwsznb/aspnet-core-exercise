using zws.Core.Abstract.Mvc;
using zws.Core.Common.Mvc;

namespace zws.Core.TestApi
{
    public class CustomApi : IRomteService
    {
        [RequestMethod(RequestMethods.GET)]
        public string GetData()
        {
            return "测试api";
        }
        [RequestMethod(RequestMethods.POST)]
        public User GetUser(User user)
        {
            return user;
        }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
