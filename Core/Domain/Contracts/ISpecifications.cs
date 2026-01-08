using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // **Query Sections**

        // For Include Expression section
        List<Expression<Func<TEntity, object>>> Includes { get; set; }



        // For Filtration Expression section
        // Null able property
        Expression<Func<TEntity, bool>>? Criteria { get; set; }



        // For OrderByAsc & OrderByDesc expression section(null-able properties)
        Expression<Func<TEntity, object>>? OrderByAsc { get; set; }
        Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
    }
}
