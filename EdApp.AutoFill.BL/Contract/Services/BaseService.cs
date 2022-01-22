using System;
using EdApp.AutoFill.DAL.Contract.Repository;

namespace EdApp.AutoFill.BL.Contract.Services
{
    public abstract class BaseService : IDisposable
    {
        private bool _disposedValue;
        protected IUnitOfWork UnitOfWork;

        /// <summary>
        ///     Initialize field unitOfWork with certain instance.
        /// </summary>
        /// <param name="unitOfWork">Instance of the unit of work</param>
        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Releases manageable resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        ~BaseService()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Releases manageable resources.
        /// </summary>
        /// <param name="disposing">Indicates either this method is invoked from Dispose() method of from finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing) GC.SuppressFinalize(this);

            UnitOfWork?.Dispose();
            UnitOfWork = null;
            _disposedValue = true;
        }
    }
}