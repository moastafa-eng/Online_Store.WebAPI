using Shard.DTOs.Products;

namespace Services.Abstractions.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId, string? sort);
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();
    }
}
