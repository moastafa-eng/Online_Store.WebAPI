using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class SpecificationsEvaluator
    {
        // **Create Query**

        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity, TKey> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // _context.Products

            // check if Criteria is not null
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); // _context.Products.Where('Filter Expression')
            }

            // aggregate => concatenation
            // _context.Products.Where('expression').Include('includeExpression').Inlude('includeExpression')+...
            spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));


            return query;
        }
    }
}
