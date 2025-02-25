using System;

namespace EcommerceMinified.Domain.Interfaces.Caching;

public interface IRedisService
{
    Task<T?> GetAsync<T>(Guid id);
    void SetAsync<T>(Guid id, T value, TimeSpan? expiration = null);
    void Remove(string key);
}
