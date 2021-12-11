using System.Data.Entity.ModelConfiguration;
using Namek.Library.Data;

namespace Namek.Data.Database
{
    public abstract class BaseEntityConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        protected BaseEntityConfiguration()
        {
            ToTable(TableName);
        }

        protected virtual string TableName => typeof(T).Name;
    }
}