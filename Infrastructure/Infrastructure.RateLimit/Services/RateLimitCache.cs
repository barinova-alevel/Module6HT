using Basket.Host.Configurations;
using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Infrastructure.RateLimit.Services
{
    internal class RateLimitCache : IRateLimitCache
    {
        private const long INIT_VALUE = 1;

        private readonly IRedisCacheConnectionService _redisCacheConnectionService;
        private readonly RateLimitConfig _config;

        public RateLimitCache(IRedisCacheConnectionService redisCacheConnectionService, IOptions<RateLimitConfig> options)
        {
            _redisCacheConnectionService = redisCacheConnectionService;
            _config = options.Value;
        }

        public async Task<long> IncrementAsync(string key)
        {
            var redis = GetRedisDatabase();

            if (!await redis.KeyExistsAsync(key)) {
                await redis.StringSetAsync(key, INIT_VALUE, _config.CacheTimeout);
                return INIT_VALUE;
            }

            return await redis.StringIncrementAsync(key);
        }

        private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase(_config.RedisDatabaseIndex);
    }
}
