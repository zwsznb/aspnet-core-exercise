using zws.Core.Abstract.Mvc;
using zws.Core.Common.Mvc;

namespace zws.Application.ApplicationService
{
    public class TestService : IRomteService
    {
        [RequestMethod]
        public string Call() 
        {
            return "ddddddddddddddddd";
        }
        
    }
}
