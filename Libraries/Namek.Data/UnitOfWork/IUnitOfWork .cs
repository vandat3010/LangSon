using Automation.Data.Repositories;
using Namek.Data.EFramework;
using Namek.Entity.EntityNewModel;
using System; 

namespace Namek.Data.UnitOfWork
{
    /// <summary>
    /// Interface UnitOfWork
    /// UnitOfWork đứng ra quản lý toàn bộ các Repository và các transaction trong 1 phiên làm việc
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        NamekContex Context { get; set; }
        IUserRepository Users { get; }
        void Save();

 
    }
}
