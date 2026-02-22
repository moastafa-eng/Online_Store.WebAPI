using Shard.DTOs.Baskets;

namespace Services.Abstractions.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string Id);
        Task<BasketDto?> CreateBasketAsync(BasketDto Dto, TimeSpan duration);
        Task<bool> DeleteBasket(string Id);
    }
}
