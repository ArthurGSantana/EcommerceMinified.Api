using System;

namespace EcommerceMinified.Domain.Interfaces.Caching;

public interface IRedisService
{
    Task<T?> GetAsync<T>(string key);
    void SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    void Remove(string key);
}
