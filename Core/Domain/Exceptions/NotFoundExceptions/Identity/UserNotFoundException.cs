using Domain.Exceptions.NotFoundExceptions;

namespace Domain.Exceptions.NotFoundEx.Identity
{
    public class UserNotFoundException(string email) : NotFoundException($"User with email [{email}] is not found")
    {
    }
}
