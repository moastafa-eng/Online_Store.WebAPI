namespace Shard.DTOs.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }


        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentIdP { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
