using Domain.Contracts;
using Domain.Entities;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // **Query Sections Implementation**
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public Expression<Func<TEntity, object>>? OrderByAsc { get; set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression) // Null able expression
        {
            Criteria = expression;
        }


        // Set OrderbyAsc & OrderBYDes with values. 
        public void SetOrderByAsc(Expression<Func<TEntity, object>> expression)
        {
            OrderByAsc = expression;
        }

        public void SetOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }
    }
}
