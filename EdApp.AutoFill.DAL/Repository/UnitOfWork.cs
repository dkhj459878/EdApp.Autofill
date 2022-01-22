using System;
using EdApp.AutoFill.DAL.Contract.Repository;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.DAL.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        protected AutoFillContext AutoFillContext;
        private bool _disposed;

        public UnitOfWork(
            AutoFillContext autoFillContext,
            IBaseRepository<ModelType> modelType,
            IBaseRepository<CalculationType> calculationType,
            IBaseRepository<Parameter> parameter, IBaseRepository<AttributeDto> attribute, IBaseRepository<AttributesForSimocalc> attributesForSimocalc, IBaseRepository<AttributeDto> attributeDto)
        {
            AutoFillContext = autoFillContext;
            ModelType = modelType;
            CalculationType = calculationType;
            Parameter = parameter;
            AttributesForSimocalc = attributesForSimocalc;
            AttributeDto = attributeDto;
        }

        public IBaseRepository<ModelType> ModelType { get; }

        public IBaseRepository<CalculationType> CalculationType { get; }

        public IBaseRepository<Parameter> Parameter { get; }

        public IBaseRepository<AttributeDto> AttributeDto { get; }

        public IBaseRepository<AttributesForSimocalc> AttributesForSimocalc { get; }

        /// <summary>
        /// Release managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Release managed resources.
        /// </summary>
        /// <param name="disposing">Indicates whether this method was called from the Dispose () method or from a finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            AutoFillContext?.Dispose();
            AutoFillContext = null;
            _disposed = true;
        }

        public int SaveChanges()
        {
            return AutoFillContext.SaveChanges();
        }
    }
}