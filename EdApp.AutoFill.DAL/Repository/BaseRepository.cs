using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EdApp.AutoFill.DAL.Contract;
using EdApp.AutoFill.DAL.Contract.Repository;
using Microsoft.EntityFrameworkCore;

namespace EdApp.AutoFill.DAL.Repository
{
    /// <summary>
    /// Represents a generic repository.
    /// The code is taken from the article https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application.
    /// </summary>
    /// <typeparam name = "TEntity"> The type that the repository is managing the instances of. </typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IIdentifier
    {
        private readonly AutoFillContext _detailDbContext;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Creates a new repository instance with the given database context.
        /// </summary>
        /// <param name = "detailDbContext"> Database context instance. </param>
        public BaseRepository(AutoFillContext detailDbContext)
        {
            _detailDbContext = detailDbContext;
            _dbSet = _detailDbContext.Set<TEntity>();
        }

        /// <summary>
        /// Returns an enumeration of <see cref = "TEntity" /> instances from the data store.
        /// Applies filtering, sorting and loading related properties,
        /// if the corresponding parameter values ​​are specified.
        /// </summary>
        /// <param name = "filter"> Lambda expression defining instance filtering <see cref = "TEntity" />. </param>
        /// <param name = "orderBy"> Lambda expression defining the sorting of instances <see cref = "TEntity" />. </param>
        /// <param name = "includeProperties"> List of related properties of instances <see cref = "TEntity" />, separated by commas. </param>
        /// <returns> Listing instances <see cref = "TEntity" />. </returns>
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            var entities = GetFilteredQuery(filter, orderBy, includeProperties).ToList();
            foreach (var entity in entities)
            {
                _detailDbContext.Entry(entity).State = EntityState.Detached;
            }
            return entities;
            
        }

        /// <summary>
        /// Returns a page of a given size with a given number
        /// as an enumeration of instances <see cref = "TEntity" /> from the data store.
        /// Applies a filter, sorting and loading related properties,
        /// if the corresponding parameter values ​​are specified.
        /// </summary>
        /// <param name = "pageSize"> Page size. </param>
        /// <param name = "pageNumber"> The page number. </param>
        /// <param name = "filter"> Lambda expression defining instance filtering <see cref = "TEntity" />. </param>
        /// <param name = "orderBy"> Lambda expression defining the sorting of instances <see cref = "TEntity" />. </param>
        /// <param name = "includeProperties"> List of related properties of instances <see cref = "TEntity" />, separated by commas. </param>
        /// <returns> Listing instances <see cref = "TEntity" />. </returns>
        public IEnumerable<TEntity> GetPage(
            int pageSize,
            int pageNumber,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            var query = GetFilteredQuery(filter, orderBy, includeProperties);
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        /// <summary>
        /// Updates an instance <see cref = "TEntity" /> with the given identifier <see cref = "TKey" />
        /// in a flat model.
        /// </summary>
        /// <param name = "entity"> Instance <see cref = "TEntity" />. </param>
        /// <param name = "id"> Instance ID Instance <see cref = "TEntity" />. </param>
        public virtual void UpdateSimpleProperties<TKey>(TEntity entity, TKey id)
        {
            _detailDbContext.Entry(_dbSet.Find(id)).CurrentValues.SetValues(entity);
        }

        /// <summary>
        /// Returns an instance of <see cref = "TEntity" />,
        /// matching the given identifier, from the data store.
        /// </summary>
        /// <param name = "id"> Identifier. </param>
        /// <typeparam name = "TKey"> Type of identifier. </typeparam>
        /// <returns> Instance <see cref = "TEntity" />. </returns>
        public TEntity GetById<TKey>(TKey id)
        {
            var entity = _dbSet.Find(id);
            _detailDbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        /// <summary>
        /// Adds the specified <see cref = "TEntity" /> instance to the data store.
        /// </ summary>
        /// <param name = "entity"> Instance <see cref = "TEntity" />.</ param>
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Attach(entity);
            _detailDbContext.Entry(entity).State = EntityState.Added;
        }
        
        /// <summary>
        /// Detaches entity.
        /// </summary>
        /// <param name="entity"></param>
        public void Detach(TEntity entity)
        {
            _detailDbContext.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Updates the specified instance <see cref = "TEntity" /> in the data store.
        /// </summary>
        /// <param name = "entity"> Instance <see cref = "TEntity" />. </param>
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _detailDbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Removes the instance <see cref = "TEntity" />,
        /// matching the given identifier, from the data store.
        /// </summary>
        /// <param name = "id"> Identifier. </param>
        /// <typeparam name = "TKey"> Type of identifier. </typeparam>
        public virtual void Delete<TKey>(TKey id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Removes all instances of <see cref = "TEntity" />.
        /// </summary>
        public virtual void DeleteAll()
        {
            _dbSet.RemoveRange(Get());
        }

        /// <summary>
        /// Removes the specified <see cref = "TEntity" /> instance from the data store.
        /// </summary>
        /// <param name = "entity"> Instance <see cref = "TEntity" />. </param>
        public virtual void Delete(TEntity entity)
        {
            if (_detailDbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Returns the number of items in the repository,
        /// matching the specified filter.
        /// </summary>
        /// <param name = "filter"> Lambda expression defining instance filtering <see cref = "TEntity" />. </param>
        /// <returns> Number of items. </returns>
        public int ItemsCount(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                return _dbSet.Count(filter);
            }

            return _dbSet.Count();
        }

        public virtual TEntity Find<TKey>(params TKey[] keyValues)
        {
            return _dbSet.Find(keyValues[0]);
        }

        public virtual IQueryable<TEntity> SelectQuery<TKey>(string query, params TKey[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters);
        }

        /// <summary>
        /// Adds a collection of <see cref = "TEntity" /> instances to the data store.
        /// </summary>
        /// <param name = "entities"> Collection of instances <see cref = "TEntity" />. </param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        /// <summary>
        /// Save changes to the database.
        /// </summary>
        /// <returns> </returns>
        public int SaveChanges()
        {

            var result = _detailDbContext.SaveChanges();
            foreach (var entity in _dbSet)
            {
                var entry = _detailDbContext.Entry(entity);
                if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Detached;
                }
            }
            return result;
        }

        private IQueryable<TEntity> GetFilteredQuery(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var includes = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return orderBy != null ? orderBy(query).AsNoTracking() : query.AsNoTracking();
        }
    }
}
