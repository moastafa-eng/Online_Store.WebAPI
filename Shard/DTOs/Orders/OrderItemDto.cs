// Ignore Spelling: Dto

namespace Shard.DTOs.Orders
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductInfoDto ProductInfo { get; set; }
    }
}