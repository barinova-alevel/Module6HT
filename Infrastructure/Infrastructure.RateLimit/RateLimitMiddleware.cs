using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Infrastructure.RateLimit
{
    public class RateLimitMiddleware
    {
        private RequestDelegate _next;
        private readonly IRateLimitService _rateLimitService;

        public RateLimitMiddleware(RequestDelegate next, IRateLimitService rateLimitService)
        {
            _next = next;
            _rateLimitService = rateLimitService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (await _rateLimitService.IsExceededAsync(context))
            {
                context.Response.ContentType = Error.ContentType;
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests; 
                await context.Response.WriteAsync(JsonConvert.SerializeObject(Error.LimitExceededProblemDetails));
                return;
            }

            await _next(context);
        }
    }
}