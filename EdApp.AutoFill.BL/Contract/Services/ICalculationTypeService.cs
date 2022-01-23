using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.BL.Contract.Services
{
    /// <summary>
    ///     Presents generic service for work with an calculation type.
    /// </summary>
    public interface ICalculationTypeService : IDisposable
    {
        /// <summary>
        ///     Returns all entities.
        /// </summary>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
        /// <param name="includeProperties">Comma-separated list of related properties of instances..</param>
        /// <returns>Calculation type enumeration.</returns>
        IEnumerable<CalculationTypeDto> GetAllCalculationTypes(
            Expression<Func<CalculationTypeDto, bool>> filter = null,
            Func<IQueryable<CalculationTypeDto>, IOrderedQueryable<CalculationTypeDto>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        ///     Returns collation of calculation type instances for one page.
        /// </summary>
        /// <param name="pageSize">Number info about calculation type, shown at the one page.</param>
        /// <param name="pageNumber">Number of the shown page with entities.</param>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
        /// <param name="includeProperties">Comma-separated list of related properties of instances..</param>
        /// <returns>Calculation type instance.</returns>
        IEnumerable<CalculationTypeDto> GetCalculationTypesPage(
            int pageSize,
            int pageNumber,
            Expression<Func<CalculationTypeDto, bool>> filter = null,
            Func<IQueryable<CalculationTypeDto>, IOrderedQueryable<CalculationTypeDto>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        ///     Returns calculation type with specific identifier.
        /// </summary>
        /// <param name="id">Calculation type identifier.</param>
        /// <returns>Calculation type instance.</returns>
        CalculationTypeDto GetCalculationType(int id);

        /// <summary>
        ///     Adds an calculation type instance into the data store.
        ///     and returns its identifier.
        /// </summary>
        /// <param name="calculationType">Calculation type instance.</param>
        /// <returns>Calculation type identifier.</returns>
        int AddCalculationType(CalculationTypeDto calculationType);

        /// <summary>
        ///     Updates certain calculation type instance into the data store.
        /// </summary>
        /// <param name="calculationType">Calculation type instance.</param>
        void UpdateCalculationType(CalculationTypeDto calculationType);

        /// <summary>
        ///     Removes calculation type with specific identifier.
        ///     from the data store.
        /// </summary>
        /// <param name="id">Calculation type identifier.</param>
        /// <returns> Task </returns>
        void DeleteCalculationType(int id);

        /// <summary>
        ///     Removes all calculation types.
        /// </summary>
        void DeleteAllCalculationTypes();

        /// <summary>
        ///     Check of availability of an calculation type in the data store,
        ///     corresponding of a defined filter.
        /// </summary>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <returns>Returns <see langword="true" />, if an calculation type exists.</returns>
        bool CalculationTypeExists(Expression<Func<CalculationTypeDto, bool>> filter);
    }
}