using Infrastracture.Abstract;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
            =>await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
            =>await _context.Set<T>().FirstOrDefaultAsync(match);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            =>await _context.Set<T>().FindAsync(id);

        public IQueryable<T> GetPridicated(Expression<Func<T, bool>> match, string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (query == null)
                return null;
            if (include != null)
                foreach (var item in include)
                    query = query.Include(item);
            return query.Where(match);
        }

        public async Task<bool> IsAny(Expression<Func<T, bool>> match)
            => await _context.Set<T>().AnyAsync(match);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);
    }
}
