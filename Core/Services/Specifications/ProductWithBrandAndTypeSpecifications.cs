using Domain.Entities.Products;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId) : base
            (
            // force the first query to be true to execute the second query, maybe second query is not null! 
                p =>
                (!brandId.HasValue || p.BrandId == brandId)
                &&
                (!typeId.HasValue || p.TypeId == typeId)
            )
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
