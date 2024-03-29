﻿using Microsoft.EntityFrameworkCore;
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

        public virtual IQueryable<T> Queryable(bool readOnly, params Expression<Func<T, object>>[] includes) {
            IQueryable<T> query = context.Set<T>();

            if (includes.Any())
                query = Include(context.Set<T>(), includes);

            if (readOnly)
                query.AsNoTracking();

            return query;
        }

        public virtual T Update(T entity) {
            context.Set<T>().Attach(entity);
            context.Entry(entity);
            return entity;
        }

        public virtual bool Exist(Func<T, bool> where, params Expression<Func<T, object>>[] includes) {
            if (includes.Any())
                return Include(context.Set<T>(), includes).Any(where);
            return context.Set<T>().Any(where);
        }

        #endregion

        #region - async -

        public virtual async Task<T> ById(bool readOnly, string id, params Expression<Func<T, object>>[] includes) {
            if (includes.Any())
                return await Queryable(readOnly, includes).FirstOrDefaultAsync(x => x.Id == id);
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> By(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) {
            return await Queryable(readOnly, includes).FirstOrDefaultAsync(where);
        }

        public virtual async Task Register(T entity) {
            await context.Set<T>().AddAsync(entity);
        }

        public virtual async Task RegisterRange(IEnumerable<T> entities) {
            await context.Set<T>().AddRangeAsync(entities);
        }

        public virtual async Task<IEnumerable<T>> List(bool readOnly, params Expression<Func<T, object>>[] includeProperties) {
            return await Queryable(readOnly, includeProperties).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListBy(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) {
            return await Queryable(readOnly, includes).Where(where).ToListAsync();
        }

        #endregion

        #region - private -

        /// <summary>
        /// Realiza include populando o objeto passado por parametro
        /// </summary>
        /// <param name="query">Informe o objeto do tipo IQuerable</param>
        /// <param name="includes">Ínforme o array de expressões que deseja incluir</param>
        /// <returns></returns>
        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includes) {
            foreach (var property in includes)
                query = query.Include(property);

            return query;
        }

        #endregion
    }
}