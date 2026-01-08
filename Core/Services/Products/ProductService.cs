using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Products;
using Services.Abstractions.Products;
using Services.Specifications;
using Shard.DTOs.Products;

namespace Services.Products
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId, string? sort, string? search, int? pageIndex, int? pageSize)
        {
            //var spec = new BaseSpecifications<Product, int>(null);
            //spec.Includes.Add(p => p.Brand);
            //spec.Includes.Add(p => p.Type);

            var spec = new ProductWithBrandAndTypeSpecifications(brandId, typeId, sort, search, pageIndex, pageSize);

            // Get all products and Mapping from Products to List of ProductResponse
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return result;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);

            // Get Product by id then mapping from product to productResponse.
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            var result = _mapper.Map<ProductResponse>(product);

            return result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            // Get all brands then mapping from brand to list of BrandTypeResponse.
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);

            return result;
        }


        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            // Get All types then mapping from types to list of BrandTypeResponse.
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);

            return result;
        }

    }
}
