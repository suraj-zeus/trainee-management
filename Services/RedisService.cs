using Microsoft.Extensions.Options;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

using Trainee.api.Configurations;

namespace Trainee.api.Services;


public class RedisService : IRedisService
{

    private IDistributedCache _cache;
    private ILogger<RedisService> _logger;
    private RedisConfig _redisConfig;

    public RedisService(
        IDistributedCache cache,
        ILogger<RedisService> logger,
        IOptions<RedisConfig> options
    )
    {
        _cache = cache;
        _logger = logger;
        _redisConfig = options.Value;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            string? cachedData = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedData))
            {
                _logger.LogInformation("Cache miss for key: {Key}", key);
                return default;
            }

            _logger.LogInformation("Cache hit for key: {Key}", key);

            return JsonSerializer.Deserialize<T>(cachedData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving key {Key} from cache", key);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value)
    {
        try
        {
            if (value == null) {
                _logger.LogWarning($"Error setting cache for key : {key}!! value was not provided..");
                return;
            }

            // Serialize the object to a JSON string
            string jsonData = JsonSerializer.Serialize(value);

            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_redisConfig.DefaultItlSeconds)
            };

            // Save to Redis 
            await _cache.SetStringAsync(key, jsonData, cacheOptions);

            _logger.LogInformation("Successfully cached key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting key {Key} in cache", key);
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {

            string? cachedData = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedData))
            {
                _logger.LogWarning("Cache value for key: {Key} does not exist..", key);
                return;
            }

            await _cache.RemoveAsync(key);
            _logger.LogInformation("Successfully removed key from cache: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing key {Key} from cache", key);
        }
    }
}