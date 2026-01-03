using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false)
        {
            // return All Fields without tracking is i want this.
            return ChangeTracker ? await _context.Set<TEntity>().ToListAsync() :
                await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

    }
}

