﻿using Microsoft.AspNetCore.Http;

namespace Infrastructure.RateLimit.Services.Interfaces
{
    public interface IRateLimitService
    {
        Task<bool> IsExceededAsync(HttpContext context);
    }
}