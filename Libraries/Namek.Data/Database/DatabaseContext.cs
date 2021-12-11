using System;
using System.Data.Entity;
using System.Linq;
using Namek.Library.Data;

namespace Namek.Data.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(string nameOrConnectionString = "NameKContext") : base(
            nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public void ExecuteSql(TransactionalBehavior? transactionalBehavior, string sqlQuery,
            params object[] parameters)
        {
            if (transactionalBehavior.HasValue)
                Database.ExecuteSqlCommand(transactionalBehavior.Value, sqlQuery, parameters);
            else
                Database.ExecuteSqlCommand(sqlQuery, parameters);
        }

        public new IDbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public bool DatabaseExists()
        {
            return Database.Exists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = typeof(DatabaseContext).Assembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}