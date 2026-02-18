using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Services.Abstractions.Auth;
using Services.Abstractions.Baskets;
using Services.Abstractions.Cache;
using Services.Abstractions.Products;
using Services.Auth;
using Services.Baskets;
using Services.Cache;
using Services.Products;
using Shard.JWT;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepository _basketRepository,
        ICacheRepository _cacheRepository,
        UserManager<AppUser> _userManger,
        IOptions<JwtOptions> _options

        ) : IServiceManager
    {
        // Add Services
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);

        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);

        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(_userManger, _options);
    }
}
