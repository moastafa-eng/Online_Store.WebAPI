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

            // **Apply filter expression if Criteria is not null**
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); // _context.Products.Where('Filter Expression')
            }

            //  **Apply OrderByAsc & OrderByDesc Expression**
            // _context.Products.Where('Filter Expression').OrderBy(p => p.Pirce)
            if (spec.OrderByAsc is not null)
            {
                query = query.OrderBy(spec.OrderByAsc);
            }

            else if(spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

                //  **Apply Include expression if includes property is not null.**
                // aggregate => concatenation
                // _context.Products.Where('expression').OrderBy(p => p.Price).Include('includeExpression').Inlude('includeExpression')+...
                query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));


            return query;
        }
    }
}
