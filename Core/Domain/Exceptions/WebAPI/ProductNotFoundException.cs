using Domain.Exceptions.NotFoundExceptions;

namespace Domain.Exceptions.WebAPI
{
    public class ProductNotFoundException(int id) : NotFoundException($"Product with id:{id} is not found!")
    {
    }
}
