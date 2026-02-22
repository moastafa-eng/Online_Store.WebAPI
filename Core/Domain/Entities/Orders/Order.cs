using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        // ParameterLess Constructor : EFCore needs the ParameterLess Constructor to know the form of class and stores it in database
        public Order() { } 


        //  Parameterized Constructor using for Business logic purpose
        public Order(string userEmail, decimal subTotal, ShippingAddress orderAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items)
        {
            UserEmail = userEmail;
            SubTotal = subTotal;
            OrderAddress = orderAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
        }




        // Properties
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal SubTotal { get; set; } // Price * Quantity

        //[NotMapped] // => Derived Attribute In DB
        //public decimal Total { get; set; } // Delivery Method Cost + SubTotal
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public ShippingAddress OrderAddress { get; set; }


        // Enumerators
        public OrderStatus Status { get; set; } = OrderStatus.Pending;



        // Navigational Properties
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; } // Fk

        public ICollection<OrderItem> Items { get; set; }

    }
}
