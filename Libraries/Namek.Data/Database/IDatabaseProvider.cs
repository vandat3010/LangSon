using System.Data.Entity.Infrastructure;

namespace Namek.Data.Database
{
    public interface IDatabaseProvider
    {
        IDbConnectionFactory ConnectionFactory { get; }
    }
}