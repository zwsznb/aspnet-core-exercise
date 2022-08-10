using zws.Core.Abstract;
using zws.Core.Common.Mvc;

namespace zws.Core.Bussiness
{
    public class RomteApi : IRomteService
    {
        [RequestMethod]
        public void Call()
        {
            Console.WriteLine("ddddddddddddddddd");
        }
        [RequestMethod(RequestMethods.POST)]
        public Animal GetData(Animal animal)
        {
            return animal;
        }
    }
}
