using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities;

namespace Vnit.ApplicationCore.Data
{
    public interface IDataRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }

        /// <summary>
        /// Gets an entity by Id
        /// </summary>
        /// <param name="id">The Id of entity</param>
        /// <returns>Entity with Id value</returns>
        T Get(int id);

        /// <summary>
        /// Gets an entity by Id loading specified related entities 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="id"></param>
        /// <param name="earlyLoad"></param>
        /// <returns></returns>
        T Get<TProperty>(int id, params Expression<Func<T, TProperty>>[] earlyLoad);

        /// <summary>
        /// Gets entities matching the passed where expression
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> where);

        IQueryable<T> Get(Expression<Func<T, bool>> @where, bool asNoTracking);

        /// <summary>
        /// Gets entities matching the passed where expression loading specified related entities 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get<TProperty>(Expression<Func<T, bool>> where, params Expression<Func<T, TProperty>>[] earlyLoad);

        /// <summary>
        /// Gets total number of entities matching where expression
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets entities matching the passed where expression asynchronously
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets entities matching the passed where expression loading specified related entities 
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<T>> GetAsync<TProperty>(Expression<Func<T, bool>> where, params Expression<Func<T, TProperty>>[] earlyLoad);

      
        void Insert(T entity, bool reloadNavigationProperties = false);

        void Insert(ICollection<T> entities, bool reloadNavigationProperties = false);

        void Update(T entity);

        void Update(ICollection<T> entities);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        void Delete(ICollection<T> entities);
    }
}
