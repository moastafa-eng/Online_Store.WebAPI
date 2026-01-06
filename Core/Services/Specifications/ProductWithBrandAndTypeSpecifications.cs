using Domain.Entities.Products;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications() : base(null) // In case : Get all without filter expression.
        {
            ApplyIncludes();
        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id) // in case GetById with filter expression.
        {
            ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
