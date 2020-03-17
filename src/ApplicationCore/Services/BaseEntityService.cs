using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities;

namespace Vnit.ApplicationCore.Services
{
    public abstract class BaseEntityService<T> : IBaseEntityService<T> where T : BaseEntity
    {
        private readonly IDataRepository<T> _dataRepository;

        protected BaseEntityService(IDataRepository<T> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public virtual void Insert(T entity, bool reloadNavigationProperties = false)
        {
            _dataRepository.Insert(entity, reloadNavigationProperties);

        }

        public virtual void Insert(ICollection<T> entities, bool reloadNavigationProperties = false)
        {
            _dataRepository.Insert(entities, reloadNavigationProperties);
        }

        public virtual void Delete(T entity)
        {
            _dataRepository.Delete(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {

            _dataRepository.Delete(where);

        }

        public virtual void Update(T entity)
        {
            _dataRepository.Update(entity);

            //publish the event so they can be handled
            //_eventPublisherService.Publish(entity, EventType.Update);
        }

        public virtual void Update(ICollection<T> entities)
        {
            _dataRepository.Update(entities);

            //publish the event so they can be handled
            //_eventPublisherService.Publish(entity, EventType.Update);
        }

        public virtual T Get(int id)
        {
            return _dataRepository.Get(x => x.Id == id).FirstOrDefault();
        }

        public T Get(int id, params Expression<Func<T, object>>[] earlyLoad)
        {
            return _dataRepository.Get(id, earlyLoad);
        }

        public virtual T First(Expression<Func<T, bool>> @where)
        {
            return _dataRepository.Get(where).First();
        }

        public virtual T First(Expression<Func<T, bool>> @where, params Expression<Func<T, object>>[] earlyLoad)
        {
            return _dataRepository.Get(where, earlyLoad).First();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            return _dataRepository.Get(where).FirstOrDefault();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> @where, params Expression<Func<T, object>>[] earlyLoad)
        {
            return _dataRepository.Get(where, earlyLoad).FirstOrDefault();
        }

        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return where == null ? _dataRepository.Count(x => true) : _dataRepository.Count(where);
        }

        #region GetPagedList
        public virtual IPagedList<T> GetPagedList(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> orderBy = null, bool ascending = false, int page = 0, int count = int.MaxValue, params Expression<Func<T, object>>[] earlyLoad)
        {
            if (where == null)
                where = (x => true);

            var resultSet = _dataRepository.Get(@where, earlyLoad);

            if (orderBy != null)
                //order
                resultSet = ascending ? resultSet.OrderBy(orderBy) : resultSet.OrderByDescending(orderBy);
            else
                resultSet = ascending ? resultSet.OrderBy(x => x.Id) : resultSet.OrderByDescending(x => x.Id);

            return new PagedList<T>(resultSet, page, count);
        }
        public virtual IPagedList<T> GetPagedList<TProperty>(Expression<Func<T, bool>> where = null, Expression<Func<T, TProperty>> orderBy = null,
            bool ascending = false, int page = 0, int count = int.MaxValue, params Expression<Func<T, object>>[] earlyLoad)
        {
            if (where == null)
                where = (x => true);

            var resultSet = _dataRepository.Get(@where, earlyLoad);

            if (orderBy != null)
            {
                resultSet = ascending ? resultSet.OrderBy(orderBy) : resultSet.OrderByDescending(orderBy);
            }
            else
                resultSet = ascending ? resultSet.OrderBy(x => x.Id) : resultSet.OrderByDescending(x => x.Id);
            return new PagedList<T>(resultSet, page, count);
        }

        public virtual async Task<IPagedList<T>> GetPagedListAsync<TProperty>(Expression<Func<T, bool>> where = null,
            Expression<Func<T, TProperty>> orderBy = null,
            bool ascending = false, int page = 0, int count = int.MaxValue, params Expression<Func<T, object>>[] earlyLoad)
        {
            return await Task.Run(() => GetPagedList(@where, orderBy, ascending, page, count, earlyLoad));
        }
        #endregion

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> orderBy = null, bool ascending = false,
            int page = 1, int count = int.MaxValue)
        {
            if (where == null)
                where = (x => true);

            var resultSet = _dataRepository.Get(@where);

            if (orderBy != null)
                //order
                resultSet = ascending ? resultSet.OrderBy(orderBy) : resultSet.OrderByDescending(orderBy);
            else
                resultSet = ascending ? resultSet.OrderBy(x => x.Id) : resultSet.OrderByDescending(x => x.Id);

            //pagination
            resultSet = resultSet.Skip((page - 1) * count).Take(count);
            return resultSet;

        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> @where = null, Expression<Func<T, object>> orderBy = null, bool @ascending = true, int page = 1,
            int count = Int32.MaxValue, params Expression<Func<T, object>>[] earlyLoad)
        {
            if (where == null)
                where = (x => true);

            var resultSet = _dataRepository.Get(@where, earlyLoad);

            if (orderBy != null)
                //order
                resultSet = ascending ? resultSet.OrderBy(orderBy) : resultSet.OrderByDescending(orderBy);
            else
                resultSet = resultSet.OrderBy(x => x.Id);

            //pagination
            resultSet = resultSet.Skip((page - 1) * count).Take(count);
            return resultSet;
        }

        public virtual async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> @where = null, Expression<Func<T, object>> orderBy = null, bool @ascending = true, int page = 1, int count = Int32.MaxValue)
        {
            return await Task.Run(() => Get(@where, orderBy, ascending, page, count));
        }

        public virtual async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> @where = null, Expression<Func<T, object>> orderBy = null, bool @ascending = true, int page = 1,
            int count = Int32.MaxValue, params Expression<Func<T, object>>[] earlyLoad)
        {
            return await Task.Run(() => Get(@where, orderBy, ascending, page, count, earlyLoad));
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where)
        {
            return await Task.Run(() => FirstOrDefault(where));
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where, params Expression<Func<T, object>>[] earlyLoad)
        {
            return await Task.Run(() => FirstOrDefault(where, earlyLoad));
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await Task.Run(() => Get(id));
        }

        public virtual async Task<T> GetAsync(int id, params Expression<Func<T, object>>[] earlyLoad)
        {
            return await Task.Run(() => Get(id, earlyLoad));
        }

        public T PreviousOrDefault(int currentEntityId, Expression<Func<T, bool>> @where = null, Expression<Func<T, object>> orderBy = null, bool @ascending = true)
        {
            return Get(where, orderBy, ascending)
                .AsEnumerable()
                .TakeWhile(x => x.Id != currentEntityId)
                .LastOrDefault();
        }

        public T NextOrDefault(int currentEntityId, Expression<Func<T, bool>> @where = null, Expression<Func<T, object>> orderBy = null, bool @ascending = true)
        {
            return Get(where, orderBy, ascending).AsEnumerable().SkipWhile(x => x.Id != currentEntityId).Skip(1).FirstOrDefault();
        }

        protected IDataRepository<T> Repository => _dataRepository;

      
    }
}
