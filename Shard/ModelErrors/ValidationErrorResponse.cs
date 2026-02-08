namespace Shard.ModelErrors
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = 400; // Bad Request.
        public string ErrorMessage { get; set; } = "Validation Errors";
        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
