using Vnit.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Vnit.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
