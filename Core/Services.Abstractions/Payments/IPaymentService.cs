using Shard.DTOs.Baskets;

namespace Services.Abstractions.Payments
{
    public interface IPaymentService
    {
        Task<BasketDto>CreatePaymentIntentAsync(string basketId);
    }
}
