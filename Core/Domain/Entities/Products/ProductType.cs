namespace Domain.Entities.Products
{
    public class ProductType : BaseEntity<int>
    {
        string Name { get; set; } = null!;
    }
}
