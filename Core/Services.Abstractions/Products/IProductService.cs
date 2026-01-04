using Shard.DTOs.Products;

namespace Services.Abstractions.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
