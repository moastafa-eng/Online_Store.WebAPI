using Domain.Entities.Baskets;
using System;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Id);
        Task<CustomerBasket?> CreateBasketAsync(CustomerBasket basket, TimeSpan duration);
        Task<bool> DeleteBasketAsync(string Id);
    }
}
