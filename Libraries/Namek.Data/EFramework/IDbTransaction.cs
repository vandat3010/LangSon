using System;
namespace Namek.Data.EFramework
{
    public interface IDbTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}