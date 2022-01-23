using System;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.DAL.Contract.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Represent a repository for an model type support.
        /// </summary>
        IBaseRepository<ModelType> ModelType { get; }

        /// <summary>
        /// Represent a repository for a an calculation type support.
        /// </summary>
        IBaseRepository<CalculationType> CalculationType { get; }

        /// <summary>
        /// Represent a repository for a parameter support.
        /// </summary>
        IBaseRepository<Model.Attribute> AttributeDto { get; }

        /// <summary>
        /// Represent a repository for attributes for Simocalc support.
        /// </summary>
        IBaseRepository<AttributesForSimocalc> AttributesForSimocalc { get; }

        /// <summary>
        /// Represent a repository for a parameter support.
        /// </summary>
        IBaseRepository<Parameter> Parameter { get; }

        /// <summary>
        /// Commit transactions.
        /// </summary>
        int SaveChanges();
    }
}