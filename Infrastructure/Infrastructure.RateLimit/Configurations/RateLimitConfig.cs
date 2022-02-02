namespace Basket.Host.Configurations;

public class RateLimitConfig
{
    public string RedisHost { get; set; } = null!;
    public int RedisDatabaseIndex { get; set; } = -1;

    public TimeSpan CacheTimeout { get; set; } = TimeSpan.FromMinutes(1);
    public int MaxRequestCount { get; set; } = 10;
}