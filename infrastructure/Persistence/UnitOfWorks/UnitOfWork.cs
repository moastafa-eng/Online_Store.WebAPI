using Domain.Contracts;
using Domain.Entities;
using Persistence.Data.DbContexts;
using Persistence.Repositories;
using System.Collections.Concurrent;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        #region GetRepository-First Method
        //private Dictionary<string, Object> _repositories = new Dictionary<string, object>();
        //public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        //{
        //    string key = typeof(TEntity).Name;

        //    // Add GenericRepository to Dictionary if it does not exist.
        //    if(!_repositories.ContainsKey(key))
        //    {
        //        var value = new GenericRepository<TEntity, TKey>(_context);
        //        _repositories.Add(key, value);
        //    }

        //    // Explicit casting from object to IGenericRepository.
        //    return (IGenericRepository<TEntity, TKey>) _repositories[key];
        //} 
        #endregion

        #region GetRepository-Second Method
        private readonly ConcurrentDictionary<string, object> _repositories = new();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Return GenericRepository if it exists Or add GenericRepository if does not exist
            return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_context));
        } 
        #endregion



        // Applying all changes on the DB
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
