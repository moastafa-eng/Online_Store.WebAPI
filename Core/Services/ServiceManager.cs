using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
using Services.Abstractions.Products;
using Services.Products;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        // Add Services
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
    }
}
