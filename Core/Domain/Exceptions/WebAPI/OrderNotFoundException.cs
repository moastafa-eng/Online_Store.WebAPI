using Domain.Exceptions.NotFoundExceptions;

namespace Domain.Exceptions.WebAPI
{
    public class OrderNotFoundException(Guid id) : NotFoundException($"Order with id {id} is not found!");
}
