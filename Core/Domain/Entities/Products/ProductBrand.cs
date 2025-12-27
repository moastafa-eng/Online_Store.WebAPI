namespace Domain.Entities.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        string Name { get; set; } = null!;
    }
}
