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
            CreateMap<BasketItem, ProductInOrderItem>();
            CreateMap<Order, OrderResponse>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.Total, O => O.MapFrom(S => S.GetTotal()));

            CreateMap<OrderAddressDto, OrderAddress>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.Product.PictureUrl))
                .ForMember(D => D.Price, O => O.MapFrom(S => S.Price))
                .ForMember(D => D.Quantity, O => O.MapFrom(S => S.Quantity));

            CreateMap<DeliveryMethod, DeliveryMethodResponse>();
        }
    }
}
