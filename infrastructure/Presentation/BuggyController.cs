using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("NotFoundError")] // BaseUrl/api/Buggy/NotFoundError
        public IActionResult NotFountErrorResponse()
        {
            // Logic

            return NotFound(); // with status code 404
        }

        [HttpGet("BadRequestError")] // BaseUrl/api/Buggy/BadRequest/Error
        public IActionResult BadRequestErrorResponse()
        {
            // Logic

            return BadRequest();// Status code 400;
        }

        [HttpGet("validationError/{id}")] // // BaseUrl/api/Buggy/ValidationError/id
        public IActionResult ValidationErrorResponse(int id)
        {
            // Logic

            return ValidationProblem(); // Status code 400
        }

        [HttpGet("ServerError")] // BaseUrl/api/Buggy/ServerError
        public IActionResult ServerErrorResponse()
        {
            // Logic

            throw new Exception();

            return BadRequest(); // Status code is 500
        }

        [HttpGet("UnauthorizedError")] // BaseUrl/api/Buggy/UnAuthorizedError
        public IActionResult UnauthorizedResponse()
        {
            // Logic

            return Unauthorized(); // Status code  401
        }

    }
}
