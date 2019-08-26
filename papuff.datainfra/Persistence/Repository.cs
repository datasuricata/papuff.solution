using Microsoft.EntityFrameworkCore;
using papuff.datainfra.ORM;
using papuff.domain.Core.Base;
using papuff.domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace papuff.datainfra.Persistence {
    public class Repository<T> : IRepository<T> where T : EntityBase {
        #region - attributes -

        private readonly AppDbContext context;

        #endregion

        #region - ctor -

        public Repository(AppDbContext context) {
            this.context = context;
        }

        #endregion

        #region - methods -

        public virtual IQueryable<T> GetQueryable() {
            return context.Set<T>();
        }

        public virtual IQueryable<T> ListBy(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties) {
            return List(readOnly, includeProperties).Where(where).AsNoTracking();
        }

        public virtual IQueryable<T> GetOrderBy<TKey>(bool readOnly, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties) {
            return ascendente ? ListBy(readOnly, where, includeProperties).OrderBy(ordem) : ListBy(readOnly, where, includeProperties).OrderByDescending(ordem);
        }

        public virtual T GetBy(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties) {
            return ListBy(readOnly, where, includeProperties).AsNoTracking().FirstOrDefault(where);
        }

        public virtual T GetById(bool readOnly, string id, params Expression<Func<T, object>>[] includeProperties) {
            return List(readOnly, includeProperties).FirstOrDefault(c => c.Id == id);
            //if (includeProperties.Any())
            //    return List(includeProperties).FirstOrDefault(x => x.Id == id);
            //return context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> List(bool readOnly, params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = context.Set<T>();

            if (includeProperties.Any())
                query = Include(context.Set<T>(), includeProperties);

            if (readOnly)
                query.AsNoTracking();

            return query;
        }

        public virtual IQueryable<T> ListOrderedBy<TKey>(bool readOnly, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties) {
            return ascendente ? List(readOnly, includeProperties).OrderBy(ordem) : List(readOnly, includeProperties).OrderByDescending(ordem);
        }

        public virtual void Register(T entity) {
            context.Set<T>().Add(entity);
        }

        public virtual T Update(T entity) {
            context.Set<T>().Attach(entity);
            context.Entry(entity);
            return entity;
        }

        public virtual T SoftDelete(T entity) {
            entity.IsDeleted = true;
            context.Set<T>().Attach(entity);
            context.Entry(entity);
            return entity;
        }

        public virtual void Delete(T entity) {
            context.Set<T>().Remove(entity);
        }

        public virtual void RegisterList(IEnumerable<T> entities) {
            context.Set<T>().AddRange(entities);
        }

        public virtual void DeleteRange(IEnumerable<T> entities) {
            context.RemoveRange(entities);
        }

        public virtual bool Exist(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties) {
            if (includeProperties.Any()) {
                return Include(context.Set<T>(), includeProperties).Any(where);
            }

            return context.Set<T>().Any(where);
        }

        #endregion

        #region - async -

        public virtual async Task<T> GetByIdAsync(bool readOnly, string id, params Expression<Func<T, object>>[] includeProperties) {
            if (includeProperties.Any())
                return await List(readOnly, includeProperties).FirstOrDefaultAsync(x => x.Id == id);
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByAsync(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties) {
            return await List(readOnly, includeProperties).FirstOrDefaultAsync(where);
        }

        public virtual async Task RegisterAsync(T entity) {
            await context.Set<T>().AddAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> ListAsync(bool readOnly, params Expression<Func<T, object>>[] includeProperties) {
            return await List(readOnly, includeProperties).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListByAsync(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties) {
            return await List(readOnly, includeProperties).Where(where).ToListAsync();
        }

        #endregion

        #region - private -

        /// <summary>
        /// Realiza include populando o objeto passado por parametro
        /// </summary>
        /// <param name="query">Informe o objeto do tipo IQuerable</param>
        /// <param name="includeProperties">Ínforme o array de expressões que deseja incluir</param>
        /// <returns></returns>
        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties) {
            foreach (var property in includeProperties)
                query = query.Include(property);

            return query;
        }

        #endregion
    }
}
