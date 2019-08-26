using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Repositories {
    public interface IRepository<T> where T : class {

        IQueryable<T> GetQueryable();
        IQueryable<T> GetOrderBy<TKey>(bool readOnly, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> List(bool readOnly, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> ListBy(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> ListOrderedBy<TKey>(bool readOnly, Expression<Func<T, TKey>> ordem, bool ascendente = true, params Expression<Func<T, object>>[] includeProperties);

        void Register(T entity);
        void RegisterList(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        T GetById(bool readOnly, string id, params Expression<Func<T, object>>[] includeProperties);
        T GetBy(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        T Update(T entity);
        T SoftDelete(T entity);

        bool Exist(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetByIdAsync(bool readOnly, string id, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByAsync(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        Task RegisterAsync(T entity);
        Task<IEnumerable<T>> ListAsync(bool readOnly, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> ListByAsync(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
    }
}
