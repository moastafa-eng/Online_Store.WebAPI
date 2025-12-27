namespace Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        string Name { get; set; } = null!;
        string Description { get; set; } = null!;
        string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
