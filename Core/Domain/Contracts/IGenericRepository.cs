using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity: BaseEntity<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<TEntity> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);


    }
}
