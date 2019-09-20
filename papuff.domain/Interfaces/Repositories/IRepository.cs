using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace papuff.domain.Interfaces.Repositories {
    public interface IRepository<T> where T : class {

        bool Exist(Func<T, bool> where, params Expression<Func<T, object>>[] includes);

        IQueryable<T> Queryable(bool readOnly, params Expression<Func<T, object>>[] includes);
        T Update(T entity);

        Task<T> ById(bool readOnly, string id, params Expression<Func<T, object>>[] includes);
        Task<T> By(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        Task Register(T entity);
        Task RegisterRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> List(bool readOnly, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> ListBy(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
    }
}