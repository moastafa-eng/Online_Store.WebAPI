using Domain.Entities.Products;
using Shard;

namespace Services.Specifications
{
    public class ProductsCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductsCountSpecifications(ProductQueryParameters parameters) : base
            (
                // force the first query to be true to execute the second query, maybe second query is not null! 
                p =>
                (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
                &&
                (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
                &&
                (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())) // Search by name
            )
        {

        }
            
            
    }
}
