
using Namek.Data.EFramework; 
using Namek.Entity.EntityNewModel;
using System;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Automation.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    { 
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(NamekContex dbContext)
            : base(dbContext)
        {
        }
         
    }
}
