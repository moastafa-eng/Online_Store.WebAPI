namespace Domain.Entities.Baskets
{
    public class CustomerBasket
    {
        // Id is a string because Redis key and value are string.
        public string Id { get; set; } 

        // every item contains about product details like price, name and quantity
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
