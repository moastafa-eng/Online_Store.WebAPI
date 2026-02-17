namespace Domain.Exceptions.BadRequestEx
{
    public class RegistrationBadRequestException(List<string> errors) : BadRequestEx($"{string.Join(",", errors)}")
    {
    }
}
