using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.BL.Contract.Services
{
    /// <summary>
    /// Presents generic service for work with an attribute.
    /// </summary>
    public interface IAttributeDtoService : IDisposable
    {
    /// <summary>
    /// Returns all entities.
    /// </summary>
    /// <param name="filter">Lambda expression defining instance filtering.</param>
    /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
    /// <param name="includeProperties">Comma-separated list of related properties of instances.</param>
    /// <returns>Attribute enumeration.</returns>
    IEnumerable<AttributeDto> GetAllAttributeDtos(
    Expression<Func<AttributeDto, bool>> filter = null,
    Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>> orderBy = null,
    string includeProperties = "");

    /// <summary>
    /// Returns collation of attribute instances for one page.
    /// </summary>
    /// <param name="pageSize">Number info about attribute, shown at the one page.</param>
    /// <param name="pageNumber">Number of the shown page with entities.</param>
    /// <param name="filter">Lambda expression defining instance filtering.</param>
    /// <param name="orderBy">A lambda expression specifying the sorting of instances.</param>
    /// <param name="includeProperties">Comma-separated list of related properties of instances.</param>
    /// <returns>Attribute instance.</returns>
    IEnumerable<AttributeDto> GetAttributeDtosPage(
    int pageSize,
    int pageNumber,
    Expression<Func<AttributeDto, bool>> filter = null,
    Func<IQueryable<AttributeDto>, IOrderedQueryable<AttributeDto>> orderBy = null,
    string includeProperties = "");

    /// <summary>
    /// Returns attribute with specific identifier.
    /// </summary>
    /// <param name="id">Attribute identifier.</param>
    /// <returns>Attribute instance.</returns>
    AttributeDto GetAttributeDto(int id);

    /// <summary>
    /// Adds an attribute instance into the data store.
    /// and returns its identifier.
    /// </summary>
    /// <param name="attributeDto">Attribute instance.</param>
    /// <returns>Attribute identifier.</returns>
    int AddAttributeDto(AttributeDto attributeDto);

    /// <summary>
    /// Updates certain attribute instance into the data store.
    /// </summary>
    /// <param name="attributeDto">Attribute instance.</param>
    void UpdateAttributeDto(AttributeDto attributeDto);

    /// <summary>
    /// Removes attribute with specific identifier.
    /// from the data store.
    /// </summary>
    /// <param name="id">Attribute identifier.</param>
    /// <returns> Task </returns>
    void DeleteAttributeDto(int id);

    /// <summary>
    /// Removes all attributes.
    /// </summary>
    void DeleteAllAttributeDtos();

    /// <summary>
    /// Check of availability of an attribute in the data store,
    /// corresponding of a defined filter.
    /// </summary>
    /// <param name="filter">Lambda expression defining instance filtering.</param>
    /// <returns>Returns <see langword="true" />, if an attribute exists.</returns>
    bool AttributeDtoExists(Expression<Func<AttributeDto, bool>> filter);
    }
}