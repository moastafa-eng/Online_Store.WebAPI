namespace Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        // Properties
        string Name { get; set; } = null!;
        string Description { get; set; } = null!;
        string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        // Navigational Properties
        public ProductBrand Brand { get; set; } = null!;
        public ProductType Type { get; set; } = null!;

        // Foreign Keys
        public int BrandId { get; set; }
        public int TypeId { get; set; }
    }
}
