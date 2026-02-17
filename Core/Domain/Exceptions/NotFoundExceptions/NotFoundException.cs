namespace Domain.Exceptions.NotFoundExceptions
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }
}
