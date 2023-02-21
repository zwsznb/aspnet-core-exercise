using zws.Core.Abstract.Mvc;
using zws.Core.Common.Mvc;

namespace zws.Test2
{
    public class Test1Service : IRomteService
    {
        [RequestMethod]
        public string Call()
        {
            return "aaaaaaaaaaaa";
        }
    }
}
