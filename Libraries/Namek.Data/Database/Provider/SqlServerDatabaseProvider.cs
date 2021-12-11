using System.Data.Entity.Infrastructure;

namespace Namek.Data.Database.Provider
{
    public class SqlServerDatabaseProvider : IDatabaseProvider
    {
        public IDbConnectionFactory ConnectionFactory => new SqlConnectionFactory();
    }
}