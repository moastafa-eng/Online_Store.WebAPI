using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
using Services.Abstractions.Baskets;
using Services.Abstractions.Cache;
using Services.Abstractions.Products;
using Services.Baskets;
using Services.Cache;
using Services.Products;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepository _basketRepository,
        ICacheRepository _cacheRepository
        ) : IServiceManager
    {
        // Add Services
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);

        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);

        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);
    }
}
