using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Contract.Services
{
    /// <summary>
    /// Presents generic service for work with an attribute.
    /// </summary>
    public interface IAttributesForSimocalcService : IDisposable
    {
    /// <summary>
    /// Returns all entities.
    /// </summary>
    /// <param name="filter">Lambda expression defining instance filtering.</param>
    /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
    /// <param name="includeProperties">Comma-separated list of related properties of instances.</param>
    /// <returns>Attribute enumeration.</returns>
    IEnumerable<AttributesForSimocalcDto> GetAllAttributesForSimocalcs(
    Expression<Func<AttributesForSimocalcDto, bool>> filter = null,
    Func<IQueryable<AttributesForSimocalcDto>, IOrderedQueryable<AttributesForSimocalcDto>> orderBy = null,
    string includeProperties = "");

    /// <summary>
    /// Returns collation of attributes for simocalc instances for one page.
    /// </summary>
    /// <param name="pageSize">Number info about attribute, shown at the one page.</param>
    /// <param name="pageNumber">Number of the shown page with entities.</param>
    /// <param name="filter">Lambda expression defining instance filtering.</param>
    /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
    /// <param name="includeProperties">Comma-separated list of related properties of instances.</param>
    /// <returns>Attribute instance.</returns>
    IEnumerable<AttributesForSimocalcDto> GetAttributesForSimocalcsPage(
    int pageSize,
    int pageNumber,
    Expression<Func<AttributesForSimocalcDto, bool>> filter = null,
    Func<IQueryable<AttributesForSimocalcDto>, IOrderedQueryable<AttributesForSimocalcDto>> orderBy = null,
    string includeProperties = "");

    /// <summary>
    /// Returns attributes for simocalc with specific identifier.
    /// </summary>
    /// <param name="id">Attribute identifier.</param>
    /// <returns>Attribute instance.</returns>
    AttributesForSimocalcDto GetAttributesForSimocalc(int id);

    /// <summary>
    /// Adds an attributes for simocalc instance into the data store.
    /// and returns its identifier.
    /// </summary>
    /// <param name="attributesForSimocalc">Attribute instance.</param>
    /// <returns>Attribute identifier.</returns>
    int AddAttributesForSimocalc(AttributesForSimocalcDto attributesForSimocalc);

    /// <summary>
    /// Updates certain attributes for simocalc instance into the data store.
    /// </summary>
    /// <param name="attributesForSimocalc">Attribute instance.</param>
    void UpdateAttributesForSimocalc(AttributesForSimocalcDto attributesForSimocalc);

    /// <summary>
    /// Removes attributes for simocalc with specific identifier.
    /// from the data store.
    /// </summary>
    /// <param name="id">Attribute identifier.</param>
    /// <returns> Task </returns>
    void DeleteAttributesForSimocalc(int id);

    /// <summary>
    /// Removes all attributes.
    /// </summary>
    void DeleteAllAttributesForSimocalcs();

    /// <summary>
    /// Check of availability of an attributes for simocalc in the data store,
    /// corresponding of a defined filter.
    /// </summary>
    /// <param name="filter">Lambda expression defining instance filtering.</param>
    /// <returns>Returns <see langword="true" />, if an attributes for simocalc exists.</returns>
    bool AttributesForSimocalcExists(Expression<Func<AttributesForSimocalcDto, bool>> filter);
    }
}