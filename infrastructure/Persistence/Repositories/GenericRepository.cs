using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false)
        {
            // return All Fields without tracking if i want this.
            if(typeof(TEntity) == typeof(Product))
            {
                return ChangeTracker ? await _context.Products.Include(b => b.Brand).Include(t => t.Type).ToListAsync() as IEnumerable<TEntity> :
                await _context.Products.Include(b => b.Brand).Include(t => t.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }

            return ChangeTracker ? await _context.Set<TEntity>().ToListAsync() :
            await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey key)
        {
            if(typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(b => b.Brand).Include(t => t.Type).FirstOrDefaultAsync(x => x.Id == key as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(key);
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

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool ChangeTracker = false)
        {
            return await SpecificationsEvaluator.GetQuery(_context.Set<TEntity>(), spec).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationsEvaluator.GetQuery(_context.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }
    }
}

