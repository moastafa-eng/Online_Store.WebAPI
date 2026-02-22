using AutoMapper;
using Domain.Entities.Baskets;
using Domain.Entities.Orders;
using Shard.DTOs.Baskets;
using Shard.DTOs.Orders;

namespace Services.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddressDto, ShippingAddress>().ReverseMap();
            CreateMap<BasketItem, ProductInOrderItem>();
            CreateMap<Order, OrderResponse>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.Total, O => O.MapFrom(S => S.GetTotal()));
                
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<ProductInOrderItem, ProductInfoDto>();
        }
    }
}
