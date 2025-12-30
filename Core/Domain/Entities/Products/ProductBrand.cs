namespace Domain.Entities.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
