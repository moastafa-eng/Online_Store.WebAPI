namespace Domain.Entities.Products
{
    internal class ProductType : BaseEntity<int>
    {
        string Name { get; set; } = null!;
    }
}
