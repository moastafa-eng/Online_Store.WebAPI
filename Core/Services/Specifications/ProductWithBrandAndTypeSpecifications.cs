using Domain.Entities.Products;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId, string? sort) : base
            (
            // force the first query to be true to execute the second query, maybe second query is not null! 
                p =>
                (!brandId.HasValue || p.BrandId == brandId)
                &&
                (!typeId.HasValue || p.TypeId == typeId)
            )
        {
            ApplyIncludes();
            ApplaySorting(sort);
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

        private void ApplaySorting(string? sort)
        {

            // if sort variable is null sort based on the name ascending 
            // if sort variable is not null => sort based on sort Variable (Ascending or Descending based on the price).
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        SetOrderByAsc(p => p.Price);
                        //OrderByAsc = p => p.Price;
                        break;

                    case "pricedesc":
                        SetOrderByDesc(p => p.Price);
                        //OrderByDesc = p => p.Price;
                        break;

                    default:
                        SetOrderByAsc(p => p.Name);
                        //OrderByAsc = p => p.Name;
                        break;
                }
            }
            else
            {
                SetOrderByAsc(p => p.Name);
                //OrderByAsc = p => p.Name;
            }
        }
    }
}
