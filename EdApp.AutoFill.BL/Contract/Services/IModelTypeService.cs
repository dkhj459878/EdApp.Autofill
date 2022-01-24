using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services
{
    /// <summary>
    ///     Presents generic service for work with an model type.
    /// </summary>
    public interface IModelTypeService : IDisposable
    {
        /// <summary>
        ///     Returns all entities.
        /// </summary>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
        /// <param name="includeProperties">Comma-separated list of related properties of instances..</param>
        /// <returns>Model type enumeration.</returns>
        IEnumerable<ModelTypeDto> GetAllModelTypes(
            Expression<Func<ModelTypeDto, bool>> filter = null,
            Func<IQueryable<ModelTypeDto>, IOrderedQueryable<ModelTypeDto>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        ///     Returns collation of model type instances for one page.
        /// </summary>
        /// <param name="pageSize">Number info about model type, shown at the one page.</param>
        /// <param name="pageNumber">Number of the shown page with entities.</param>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
        /// <param name="includeProperties">Comma-separated list of related properties of instances..</param>
        /// <returns>Model type instance.</returns>
        IEnumerable<ModelTypeDto> GetModelTypesPage(
            int pageSize,
            int pageNumber,
            Expression<Func<ModelTypeDto, bool>> filter = null,
            Func<IQueryable<ModelTypeDto>, IOrderedQueryable<ModelTypeDto>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        ///     Returns model type with specific identifier.
        /// </summary>
        /// <param name="id">Model type identifier.</param>
        /// <returns>Model type instance.</returns>
        ModelTypeDto GetModelType(int id);

        /// <summary>
        ///     Adds an model type instance into the data store.
        ///     and returns its identifier.
        /// </summary>
        /// <param name="modelType">Model type instance.</param>
        /// <returns>Model type identifier.</returns>
        int AddModelType(ModelTypeDto modelType);

        /// <summary>
        ///     Updates certain model type instance into the data store.
        /// </summary>
        /// <param name="modelType">Model type instance.</param>
        void UpdateModelType(ModelTypeDto modelType);

        /// <summary>
        ///     Removes model type with specific identifier.
        ///     from the data store.
        /// </summary>
        /// <param name="id">Model type identifier.</param>
        /// <returns> Task </returns>
        void DeleteModelType(int id);

        /// <summary>
        ///     Removes all model types.
        /// </summary>
        void DeleteAllModelTypes();

        /// <summary>
        ///     Check of availability of an model type in the data store,
        ///     corresponding of a defined filter.
        /// </summary>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <returns>Returns <see langword="true" />, if an model type exists.</returns>
        bool ModelTypeExists(Expression<Func<ModelTypeDto, bool>> filter);
    }
}