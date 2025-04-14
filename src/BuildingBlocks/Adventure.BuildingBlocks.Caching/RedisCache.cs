using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

namespace Adventure.BuildingBlocks.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await _cache.GetStringAsync(key, cancellationToken);
        
        if (string.IsNullOrEmpty(cachedValue))
            return null;
            
        return JsonSerializer.Deserialize<T>(cachedValue, _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class
    {
        var options = new DistributedCacheEntryOptions();
        
        if (expiry.HasValue)
            options.AbsoluteExpirationRelativeToNow = expiry;
        else
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // Default expiry
            
        var serializedValue = JsonSerializer.Serialize(value, _jsonOptions);
        await _cache.SetStringAsync(key, serializedValue, options, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(key, cancellationToken);
    }
}
