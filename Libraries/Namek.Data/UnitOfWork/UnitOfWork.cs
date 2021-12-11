using Automation.Data.Repositories;
using Namek.Data.EFramework;
using Namek.Entity.EntityNewModel;
using System;

namespace Namek.Data.UnitOfWork
{
    /// <summary>
    /// Quản lý các repository và context kết nối đến csdl
    /// Khởi tạo transaction
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        public NamekContex Context { get; set; }

        public UnitOfWork()
        {
            InitializeContext();
        }

        /// <summary>
        /// Khởi tạo kết nối
        /// </summary>
        protected void InitializeContext()
        {
            if (Context == null)
            {
                Context = new NamekContex();
                Context.Configuration.LazyLoadingEnabled = true;
            }
        }


        #region SAVE AND DISPOSE DATA
        public IUserRepository Users { get { return new UserRepository(Context); } }
        /// <summary>
        /// Lưu dữ liệu xuống server
        /// </summary>
        public void Save()
        {
            Context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Lưu vào giải phóng kết nối
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion SAVE AND DISPOSE DATA
    }
}
