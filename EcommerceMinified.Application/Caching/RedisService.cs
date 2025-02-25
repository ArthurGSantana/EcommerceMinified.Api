using System;
using System.Text.Json;
using EcommerceMinified.Domain.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace EcommerceMinified.Application.Caching;

public class RedisService(IDistributedCache _distributedCache) : IRedisService
{
    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _distributedCache.GetStringAsync(key);
        return value is null ? default : JsonSerializer.Deserialize<T>(value);
    }

    public void SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions();

        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration;
        }

        _distributedCache.SetString(key, JsonSerializer.Serialize(value), options);
    }

    public void Remove(string key)
    {
        _distributedCache.Remove(key);
    }
}
