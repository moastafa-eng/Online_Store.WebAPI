namespace Domain.Entities.Orders
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public  string Description { get; set; }
        public DateTime DeliveyTime { get; set; }
        public decimal Cost { get; set; }
    }
}