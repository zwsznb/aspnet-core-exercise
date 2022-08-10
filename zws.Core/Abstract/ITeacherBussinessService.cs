using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zws.Core.Common;

namespace zws.Core.Abstract
{
    public interface ITeacherBussinessService : IBussinessService
    {
        void GetTeacher();
    }
}
