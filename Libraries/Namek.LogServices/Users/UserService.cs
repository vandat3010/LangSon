using Namek.Library.Data;
using Namek.Library.Entity.Users;
using Namek.Library.Services;

namespace Namek.LogServices.Users
{
    public class UserService : BaseEntityService<User>, IUserService
    {
        public UserService(IDataRepository<User> dataRepository) : base(dataRepository) { }
    }
}