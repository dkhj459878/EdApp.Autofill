using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EdApp.AutoFill.DAL.Contract.Repository
{
    /// <summary>
    /// Represents a generic repository
    /// </summary>
    /// <typeparam name="TEntity">The type that the repository manages the instances of</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, IIdentifier
    {
        /// <summary>
        /// Returns an enumeration of <see cref = "TEntity" /> instances from the data store.
        /// Applies filtering, sorting and loading related properties,
        /// if the corresponding parameter values are specified
        /// </summary>
        /// <param name = "filter"> Lambda expression defining instance filtering <see cref = "TEntity" /> </param>
        /// <param name = "orderBy"> Lambda expression defining the sorting of instances <see cref = "TEntity" /> </param>
        /// <param name = "includeProperties"> List of related properties of instances <see cref = "TEntity" />, separated by commas </param>
        /// <returns> Listing instances <see cref = "TEntity" /> </returns>
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Returns a page of a given size with a given number
        /// as an enumeration of instances <see cref = "TEntity" /> from the data store.
        /// Applies filtering, sorting and loading related properties,
        /// if the corresponding parameter values are specified
        /// </summary>
        /// <param name = "pageSize"> Page size </param>
        /// <param name = "pageNumber"> Page number </param>
        /// <param name = "filter"> Lambda expression defining instance filtering <see cref = "TEntity" /> </param>
        /// <param name = "orderBy"> Lambda expression defining the sorting of instances <see cref = "TEntity" /> </param>
        /// <param name = "includeProperties"> Comma-separated list of related properties of instances <see cref = "TEntity" /> </param>
        /// <returns> Lists instances. <see cref = "TEntity" /> </returns>
        IEnumerable<TEntity> GetPage(
            int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Updates an instance <see cref = "TEntity" /> with the given identifier <see cref = "TKey" />
        /// in a flat model.
        /// </summary>
        /// <param name = "entity"> Instance <see cref = "TEntity" />. </param>
        /// <param name = "id"> Instance ID Instance <see cref = "TEntity" />. </param>
        void UpdateSimpleProperties<TKey>(TEntity entity, TKey id);

        /// <summary>
        /// Returns an instance of <see cref = "TEntity" />,
        /// matching the given identifier, from the data store.
        /// </summary>
        /// <typeparam name = "TKey"> Type of identifier. </typeparam>
        /// <param name = "id"> Instance ID. </param>
        /// <returns> Instance <see cref = "TEntity" />. </returns>
        TEntity GetById<TKey>(TKey id);

        /// <summary>
        /// Adds the given instance <see cref = "TEntity" /> to the datastore.
        /// </summary>
        /// <param name = "entity"> Element instance. <see cref = "TEntity" />. </param>
        void Insert(TEntity entity);

        /// <summary>
        /// Detaches entity.
        /// </summary>
        /// <param name="entity"></param>
        void Detach(TEntity entity);

        /// <summary>
        /// Updates the specified instance <see cref = "TEntity" /> in the datastore.
        /// </summary>
        /// <param name = "entity"> An entity instance. <see cref = "TEntity" /> </param>
        void Update(TEntity entity);

        /// <summary>
        /// Removes the instance <see cref = "TEntity" />,
        /// matching the given identifier, from the data store.
        /// </summary>
        /// <typeparam name = "TKey"> Type of identifier. </typeparam>
        /// <param name = "id"> Instance ID. </param>
        void Delete<TKey>(TKey id);

        /// <summary>
        /// Removes all instances of <see cref = "TEntity" />.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Removes the specified <see cref = "TEntity" /> instance from the datastore
        /// </summary>
        /// <param name = "entity"> An entity instance. <see cref = "TEntity" /> </param>
        void Delete(TEntity entity);

        /// <summary>
        /// Returns the number of items in the repository,
        /// matching the specified filter.
        /// </summary>
        /// <param name = "filter"> Lambda expression defining instance filtering <see cref = "TEntity" />. </param>
        /// <returns> Number of items </returns>
        int ItemsCount(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Search by primary key
        /// </summary>
        /// <typeparam name = "TKey"> model </typeparam>
        /// <param name = "keyValues"> params </param>
        /// <returns> Entity. </returns>
        TEntity Find<TKey>(params TKey[] keyValues);

        /// <summary>
        /// Returns IQueryable
        /// </summary>
        /// <typeparam name="TKey"> model </typeparam>
        /// <param name="query"> string </param>
        /// <param name="parameters"> params </param>
        /// <returns> IQueryable</returns>
        IQueryable<TEntity> SelectQuery<TKey>(string query, params TKey[] parameters);

        /// <summary>
        /// Insert collection
        /// </summary>
        /// <param name = "entities"> Collection of records to insert </param>
        void InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Returns a Queryable entity
        /// </summary>
        /// <returns> IQueryable </returns>
        IQueryable<TEntity> Queryable();

        int SaveChanges();
    }
}

