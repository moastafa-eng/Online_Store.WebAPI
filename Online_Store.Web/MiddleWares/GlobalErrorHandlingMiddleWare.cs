using Domain.Exceptions.BadRequestEx;
using Domain.Exceptions.NotFoundExceptions;
using Domain.Exceptions.UnauthorizedExceptions;
using Shard.ModelErrors;

namespace Online_Store.Web.MiddleWares
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next; // _next : contains the address of the next middle ware

        public GlobalErrorHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        // HttpContext : contains the information about Http request and Http response
        public async Task InvokeAsync(HttpContext context) 
        {
            // Try to transfer the Http response to the next middle ware
            // if an exception occurs while this operation return response with specific specifications.
            try
            {
                await _next.Invoke(context);

                // Catch the response from Routing Middle Ware and change response body.
                if(context.Response.StatusCode is StatusCodes.Status404NotFound)
                {
                    // Set content type.
                    context.Response.ContentType = "Application/Json";

                    // set body of the response
                    var response = new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $"The end point with name '{context.Request.Path}' is not found!"
                    };

                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch(Exception ex)
            {
                // 1.Set Status Code
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestEx => StatusCodes.Status400BadRequest,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    _=> StatusCodes.Status500InternalServerError
                };

                // 2.Set Content Type
                context.Response.ContentType = "Application/Json";

                // 3.Set Body Of Response
                var response = new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                // return Response
                await context.Response.WriteAsJsonAsync(response); //  return response with Json format
            }
        }
    }
}
