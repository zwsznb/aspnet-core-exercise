using zws.Core.Abstract.Repository;
using zws.Core.Entity;

namespace zws.Core.Abstract
{
    public interface IUserRespository : IRespository
    {
        User GetUser();
    }
}
