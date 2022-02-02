using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.RateLimit
{
    internal static class Error
    {
        public static ProblemDetails LimitExceededProblemDetails { get; } = new ProblemDetails { Title = "Rate limit exceeded" };
        public static string ContentType { get; } = "application/problem+json";
    }
}
