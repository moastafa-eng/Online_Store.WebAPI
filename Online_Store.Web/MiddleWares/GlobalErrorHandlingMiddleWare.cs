using Domain.Exceptions.NotFoundEx;
using Microsoft.AspNetCore.Http;
using Shard.ModelErrors;

namespace Online_Store.Web.MiddleWares
{
    public class GlobalErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                // 1.Set Status Code
                context.Response.StatusCode = ex switch
                {
                    ProductNotFoundEx => StatusCodes.Status404NotFound,
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
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
