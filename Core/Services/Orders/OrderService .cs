using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Exceptions.BadRequestEx;
using Domain.Exceptions.WebAPI;
using Services.Abstractions.Orders;
using Services.Specifications.Orders;
using Shard.DTOs.Orders;

namespace Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponse> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // Get Order Address
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);



            // Get Delivery Method By Id
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundEx(request.DeliveryMethodId);

            



            // Get Order Items
            // 1.Get basket by id
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            // 2.Convert every basket item to order item
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                // Check Product Price
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                if (product.Price != item.Price) item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);

                orderItems.Add(orderItem);
            }


            // Calculate SubTotal
            var subTototal = orderItems.Sum(OI => OI.Price * OI.Quantity);

            var order = new Order(userEmail, subTototal, orderAddress, deliveryMethod, orderItems, basket.PaymentIntentId);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);

            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CreateOrderBadRequestEx();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
        }

        public async Task<OrderResponse> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var specs = new OrderSpecifications(id, userEmail);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specs);

            if (order is null) throw new OrderNotFoundException(id);

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {

            var specs = new OrderSpecifications(userEmail);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(specs);

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}
