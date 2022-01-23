using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EdApp.AutoFill.BL.Model;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.BL.Contract.Services
{
    /// <summary>
    ///     Presents generic service for work with an parameter.
    /// </summary>
    public interface IParameterService : IDisposable
    {
        /// <summary>
        ///     Returns all entities.
        /// </summary>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
        /// <param name="includeProperties">Comma-separated list of related properties of instances..</param>
        /// <returns>Parameter enumeration.</returns>
        IEnumerable<ParameterDto> GetAllParameters(
            Expression<Func<ParameterDto, bool>> filter = null,
            Func<IQueryable<ParameterDto>, IOrderedQueryable<ParameterDto>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        ///     Returns collation of parameter instances for one page.
        /// </summary>
        /// <param name="pageSize">Number info about parameter, shown at the one page.</param>
        /// <param name="pageNumber">Number of the shown page with entities.</param>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
        /// <param name="includeProperties">Comma-separated list of related properties of instances..</param>
        /// <returns>Parameter instance.</returns>
        IEnumerable<ParameterDto> GetParametersPage(
            int pageSize,
            int pageNumber,
            Expression<Func<ParameterDto, bool>> filter = null,
            Func<IQueryable<ParameterDto>, IOrderedQueryable<ParameterDto>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        ///     Returns parameter with specific identifier.
        /// </summary>
        /// <param name="id">Parameter identifier.</param>
        /// <returns>Parameter instance.</returns>
        ParameterDto GetParameter(int id);

        /// <summary>
        ///     Adds an parameter instance into the data store.
        ///     and returns its identifier.
        /// </summary>
        /// <param name="modelType">Parameter instance.</param>
        /// <returns>Parameter identifier.</returns>
        int AddParameter(ParameterDto modelType);

        /// <summary>
        ///     Updates certain parameter instance into the data store.
        /// </summary>
        /// <param name="modelType">Parameter instance.</param>
        void UpdateParameter(ParameterDto modelType);

        /// <summary>
        ///     Removes parameter with specific identifier.
        ///     from the data store.
        /// </summary>
        /// <param name="id">Parameter identifier.</param>
        /// <returns> Task </returns>
        void DeleteParameter(int id);

        /// <summary>
        ///     Removes all parameters.
        /// </summary>
        void DeleteAllParameters();

        /// <summary>
        ///     Check of availability of an parameter in the data store,
        ///     corresponding of a defined filter.
        /// </summary>
        /// <param name="filter">Lambda expression defining instance filtering.</param>
        /// <returns>Returns <see langword="true" />, if an parameter exists.</returns>
        bool ParameterExists(Expression<Func<ParameterDto, bool>> filter);
    }
}