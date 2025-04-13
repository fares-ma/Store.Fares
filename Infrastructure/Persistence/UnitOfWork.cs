using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private readonly Dictionary<string,object > _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
               var repository = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(type, repository);
            }
            return (IGenericRepository<TEntity, TKey>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
