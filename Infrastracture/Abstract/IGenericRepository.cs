using Infrastracture.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> GetPridicated(Expression<Func<T, bool>> match, string[] include = null!);
        Task<bool> IsAny(Expression<Func<T, bool>> match);

        IQueryable<T> GetEntityWithSpecification(ISpecification<T> specs);
        Task<T> GetEntityByIdSepecification(ISpecification<T> specification);
        Task<T>FindIgnoreQueryFilter(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> GetAllAsyncIgnoreQueryFilter();

    }
}
