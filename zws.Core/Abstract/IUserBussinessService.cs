using System;
using zws.Core.Abstract.Mvc;

namespace zws.Core.Abstract
{
    public interface IUserBussinessService : IBussinessService
    {
        void CallUser();
    }
}
