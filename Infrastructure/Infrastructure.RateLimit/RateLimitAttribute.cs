using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.RateLimit
{
    public class RateLimitAttribute : TypeFilterAttribute
    {
        public RateLimitAttribute() : base(typeof(RateLimitFilter))
        {
        }
    }

    public class RateLimitFilter : IAsyncResourceFilter
    {
        private readonly IRateLimitService _rateLimitService;

        public RateLimitFilter(IRateLimitService rateLimitService)
        {
            _rateLimitService = rateLimitService;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (await _rateLimitService.IsExceededAsync(context.HttpContext))
            {
                context.Result = new ObjectResult(Error.LimitExceededProblemDetails)
                {
                    ContentTypes = { Error.ContentType },
                    StatusCode = StatusCodes.Status429TooManyRequests
                };

                return;
            }

            await next();
        }
    }
}
