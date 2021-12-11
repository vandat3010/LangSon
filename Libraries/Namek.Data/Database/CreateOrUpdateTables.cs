using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Namek.Data.Database
{
    public class CreateOrUpdateTables<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
            var databaseExists = context.Database.Exists();
            if (!databaseExists)
                throw new Exception("Database not found");

            //create all tables
            var dbCreationScript = ((IObjectContextAdapter) context).ObjectContext.CreateDatabaseScript();
            context.Database.ExecuteSqlCommand(dbCreationScript);

            //Seed(context);
            context.SaveChanges();
        }
    }
}