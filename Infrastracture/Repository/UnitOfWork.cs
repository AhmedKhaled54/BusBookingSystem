using Infrastracture.Abstract;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private   Hashtable _hashtable;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task <int>Complete()
            =>await  _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_hashtable is  null)
                _hashtable= new Hashtable();

            var key = typeof(T).Name;
            
            if (!_hashtable.ContainsKey(key))
            {
                var type = typeof(GenericRepository<>).MakeGenericType(typeof(T));
                var instance = Activator.CreateInstance(type, _context);
                _hashtable.Add(key, instance);
            } 
            return (IGenericRepository<T>)_hashtable[key];
        }

        public async Task<IDbContextTransaction> TransactionAsync()
            =>await _context.Database.BeginTransactionAsync();
    }
}
