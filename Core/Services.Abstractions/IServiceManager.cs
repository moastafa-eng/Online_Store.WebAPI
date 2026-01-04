using Services.Abstractions.Products;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
    }
}
