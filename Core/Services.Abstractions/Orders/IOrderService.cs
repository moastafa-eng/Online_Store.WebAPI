using Shard.DTOs.Orders;

namespace Services.Abstractions.Orders
{
    public interface IOrderService
    {
        // Catch UserEmail from the Token
        Task<OrderResponse> CreateOrderAsync(OrderRequest request, string userEmail);
        Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync();
        Task<OrderResponse> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail);
        Task<IEnumerable<OrderResponse>> GetOrdersByIdForSpecificUserAsync(string UserEmail);
    }
}
