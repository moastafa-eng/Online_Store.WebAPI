using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool ChangeTracker = false); 
        Task<TEntity?> GetByIdAsync(TKey key);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> spec); 
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
