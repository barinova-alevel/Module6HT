﻿using Basket.Host.Configurations;
using Infrastructure.RateLimit.Services;
using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RateLimit.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRateLimit(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RateLimitConfig>(configuration.GetSection("RateLimit"));
            services.AddSingleton<IRateLimitService, RateLimitService>();
            services.AddSingleton<IRateLimitCache, RateLimitCache>();
            services.AddSingleton<IRedisCacheConnectionService, RedisCacheConnectionService>();
            return services;
        }
    }
}
