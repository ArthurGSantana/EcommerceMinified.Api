using System;
using System.Text.Json;
using EcommerceMinified.Domain.Interfaces.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace EcommerceMinified.Application.Caching;

public class RedisService(IDistributedCache _distributedCache) : IRedisService
{
    public async Task<T?> GetAsync<T>(Guid id)
    {
        var key = $"{nameof(T)}_{id}";
        var value = await _distributedCache.GetStringAsync(key);
        return value is null ? default : JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(Guid id, T value, TimeSpan? expiration = null)
    {
        var key = $"{nameof(T)}_{id}";
        var options = new DistributedCacheEntryOptions();

        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration;
        }

        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
    }

    public void Remove(string key)
    {
        _distributedCache.Remove(key);
    }
}
