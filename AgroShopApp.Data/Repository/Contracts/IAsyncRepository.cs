﻿using System.Linq.Expressions;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface IAsyncRepository<TEntity, TKey>
    {
        ValueTask<TEntity?> GetByIdAsync(TKey id);

        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindByConditionsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync();

        Task AddAsync(TEntity item);

        Task AddRangeAsync(IEnumerable<TEntity> items);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> HardDeleteAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity item);

        Task SaveChangesAsync();
    }
}
