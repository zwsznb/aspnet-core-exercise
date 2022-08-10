using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zws.Core.Abstract;
using zws.Core.Common;

namespace zws.Core.Bussiness
{
    [Expose(typeof(ITeacherBussinessService))]
    public class TeacherBussinessService : Test, ITeacherBussinessService
    {
        public void GetTeacher()
        {
            Console.WriteLine("调用获取教师接口");
        }

        public void test1()
        {
            throw new NotImplementedException();
        }
    }
    public interface Test { void test1(); }
}
