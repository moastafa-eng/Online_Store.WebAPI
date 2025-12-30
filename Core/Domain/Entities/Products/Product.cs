namespace Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        // Properties
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string PictureUrl { get; set; } 
        public decimal Price { get; set; }

        // Navigational Properties
        public ProductBrand Brand { get; set; }
        public ProductType Type { get; set; }

        // Foreign Keys
        public int BrandId { get; set; }
        public int TypeId { get; set; }
    }
}
