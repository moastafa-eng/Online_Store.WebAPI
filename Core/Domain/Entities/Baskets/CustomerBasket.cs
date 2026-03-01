namespace Domain.Entities.Baskets
{
    public class CustomerBasket
    {
        // Id is a string because Redis key and value are string.
        public string Id { get; set; } 

        // every item contains a product details like price, name and quantity
        public IEnumerable<BasketItem> Items { get; set; }


        
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }

    }
}
