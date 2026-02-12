using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System.Text;

namespace Presentation.Attributes
{
    public class CacheAttribute(int timeInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            var cacheKey = GetCacheKey(context.HttpContext.Request);

            var result = await cacheService.GetAsync(cacheKey);

            if(!string.IsNullOrEmpty(result))
            {
                var response = new ContentResult
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                context.Result = response;
                return;
            }

            var actionResult = await next.Invoke();

            if(actionResult.Result is OkObjectResult okObjectResult)
            {
                await cacheService.SetAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(timeInSec));
            }
        }

        private string GetCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query)
            {
                key.Append($"|{item.Key}-{item.Value}");
            }

            return key.ToString();
        }
    }
}
