namespace Domain.Entities.Orders
{
    public enum OrderStatus
    {
        Pending = 1,
        PaymentReceived,
        PaymentFailed
    }
}