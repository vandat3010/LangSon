using System.Data.Entity;
using Namek.Library.Data;

namespace Namek.Data.Database
{
    public interface IDatabaseContext
    {
        System.Data.Entity.Database Database { get; }
        IDbSet<T> Set<T>() where T : BaseEntity;

        void ExecuteSql(TransactionalBehavior? transactionalBehavior, string sqlQuery, params object[] parameters);

        int SaveChanges();

        bool DatabaseExists();
    }
}