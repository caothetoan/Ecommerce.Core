using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.Infrastructure.Data
{
    public class DataRepository<T> : IDataRepository<T> where T : BaseEntity
    {
        private DbSet<T> _entityDbSet;

        private readonly IDatabaseContext _databaseContext;
        public DataRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        private void _SetupContexts()
        {

            _entityDbSet = _databaseContext.Set<T>();
        }
        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entityDbSet == null)
                    _entityDbSet = _databaseContext.Set<T>();
                return _entityDbSet;
            }
        }

        public T Get(int id)
        {
            _SetupContexts();
            return _entityDbSet.FirstOrDefault(x => x.Id == id);
        }

        public T Get<TProperty>(int id, params Expression<Func<T, TProperty>>[] earlyLoad)
        {
            _SetupContexts();
            var dbSet = _entityDbSet.AsQueryable();
            dbSet = earlyLoad.Aggregate(dbSet, (current, el) => current.Include(el));

            return dbSet.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> @where)
        {
            _SetupContexts();
            where = AppendSoftDeletableCondition(where);
            return _entityDbSet.Where(where);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> @where, bool asNoTracking)
        {
            _SetupContexts();
            where = AppendSoftDeletableCondition(where);
            return asNoTracking ? _entityDbSet.AsNoTracking().Where(where) : _entityDbSet.Where(where);
        }

        public IQueryable<T> Get<TProperty>(Expression<Func<T, bool>> @where,
            params Expression<Func<T, TProperty>>[] earlyLoad)
        {
            _SetupContexts();
            where = AppendSoftDeletableCondition(where);
            var dbSet = _entityDbSet.AsQueryable();
            dbSet = earlyLoad.Aggregate(dbSet, (current, el) => current.Include(el));
            return dbSet.Where(where);
        }

        public int Count(Expression<Func<T, bool>> @where)
        {
            _SetupContexts();
            where = AppendSoftDeletableCondition(where);
            return _entityDbSet.Count(where);
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> @where)
        {
            return await Task.Run(() => Get(@where));
        }

        public async Task<IQueryable<T>> GetAsync<TProperty>(Expression<Func<T, bool>> @where,
            params Expression<Func<T, TProperty>>[] earlyLoad)
        {
            return await Task.Run(() => Get(@where, earlyLoad));
        }


        public void Insert(T entity, bool reloadNavigationProperties = false)
        {
            _SetupContexts();
            if (entity == null)
                throw new ArgumentNullException();

            try
            {
                _entityDbSet.Add(entity);
                _databaseContext.SaveChanges();
                if (reloadNavigationProperties)
                    //reload the entity to load the navigation properties
                    ReloadWithNavigationProperties(entity);
            }
            catch (DbException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public void Insert(ICollection<T> entities, bool reloadNavigationProperties = false)
        {
            _SetupContexts();
            if (entities == null)
                throw new ArgumentNullException();

            try
            {
                foreach (var entity in entities)
                {
                    _entityDbSet.Add(entity);

                }
                _databaseContext.SaveChanges();
            }
            catch (DbException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        public void Update(T entity)
        {
            _SetupContexts();
            if (entity == null)
                throw new ArgumentNullException();

            try
            {
                _databaseContext.SaveChanges();
            }
            catch (DbException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public void Update(ICollection<T> entities)
        {
            _SetupContexts();
            if (entities == null)
                throw new ArgumentNullException();

            try
            {
                _databaseContext.SaveChanges();
            }
            catch (DbException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            try
            {
                //if it's soft deletable, we should just set deleted to true, instead of deleting 
                var deletable = entity as ISoftDeletable;
                if (deletable != null)
                {
                    deletable.Deleted = true;
                    Update(entity);
                    return;
                }
                _SetupContexts();
                _entityDbSet.Attach(entity);
                _entityDbSet.Remove(entity);

                _databaseContext.SaveChanges();
            }
            catch (DbException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        public void Delete(ICollection<T> entities)
        {
            _SetupContexts();
            if (entities == null)
                throw new ArgumentNullException();

            try
            {
                foreach (var entity in entities)
                {
                    var deletable = entity as ISoftDeletable;
                    if (deletable != null)
                    {
                        deletable.Deleted = true;
                        Update(entity);
                        return;
                    }

                    _entityDbSet.Remove(entity);
                }
                _databaseContext.SaveChanges();
            }
            catch (DbException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
        public void Delete(Expression<Func<T, bool>> @where)
        {
            _SetupContexts();

            try
            {
                var entities = _entityDbSet.Where(where).ToList();
                foreach (var entity in entities)
                {
                    var deletable = entity as ISoftDeletable;
                    if (deletable != null)
                    {
                        deletable.Deleted = true;
                        Update(entity);
                        continue;
                    }

                    _entityDbSet.Remove(entity);

                }

                _databaseContext.SaveChanges();
            }
            catch (DbException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        private Expression<Func<T, bool>> AppendSoftDeletableCondition(Expression<Func<T, bool>> where)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
            {
                //the parameter
                var param = Expression.Parameter(typeof(T), "x");
                var deletedWhere =
                    Expression.Lambda<Func<T, bool>>(
                        Expression.Equal(Expression.Property(param, "Deleted"), Expression.Constant(false)), param);

                //combine these to create a single expression
                where = ExpressionHelpers.CombineAnd<T>(where, deletedWhere);
            }
            return where;
        }

        private void ReloadWithNavigationProperties(T entity)
        {
            //var oc = ((IObjectContextAdapter)_databaseContext).ObjectContext;
            ////we first detach the entity and get it again from the database to perform the reload
            //oc.Detach(entity); //detach entity to retrieve entity with navigation properties
            //var refreshedEntity = Get(entity.Id);

            //var fields = entity.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            //foreach (var field in fields)
            //{
            //    var value = field.GetValue(refreshedEntity);
            //    //assign to the original object
            //    field.SetValue(entity, value);
            //}

            //var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //foreach (var property in properties)
            //{
            //    var value = property.GetValue(refreshedEntity);
            //    //assign to the original object
            //    property.SetValue(entity, value);
            //}
        }
    }
}
