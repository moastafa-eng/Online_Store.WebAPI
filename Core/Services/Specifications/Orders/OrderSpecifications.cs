using Domain.Entities.Orders;

namespace Services.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(Guid id , string userEmail) : base(O => O.Id == id &&  O.UserEmail == userEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }

        public OrderSpecifications(string userEmail) : base(O => O.UserEmail == userEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);

            SetOrderByDesc(O => O.OrderDate);
        }
    }
}
