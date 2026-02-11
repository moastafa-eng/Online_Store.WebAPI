namespace Domain.Exceptions.NotFoundEx
{
    public class BasketNotFoundEx(string Id) : NotFoundEx($"Basket with id {Id} is not found!") { }
}
