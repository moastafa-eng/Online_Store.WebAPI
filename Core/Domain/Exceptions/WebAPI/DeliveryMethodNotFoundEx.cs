using Domain.Exceptions.NotFoundExceptions;

namespace Domain.Exceptions.WebAPI
{
    public class DeliveryMethodNotFoundEx(int id) : NotFoundException($"Delivery method with id {id} is not found!");
}
