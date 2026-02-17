using Domain.Exceptions.NotFoundExceptions;

namespace Domain.Exceptions.WebAPI
{
    public class BasketNotFoundException(string Id) : NotFoundException($"Basket with id {Id} is not found!") { }
}
