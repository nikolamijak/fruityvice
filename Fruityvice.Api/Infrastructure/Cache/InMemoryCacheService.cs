using Fruityvice.Api.Application.Ports;
using Microsoft.Extensions.Caching.Memory;

namespace Fruityvice.Api.Infrastructure.Cache;

public class InMemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    public ValueTask<T?> GetAsync<T>(string key)
     => ValueTask.FromResult(memoryCache.Get<T>(key) ?? default);
    

    public ValueTask InvalidateAsync(string key)
    {
        memoryCache.Remove(key);
        return ValueTask.CompletedTask;
    }

    public ValueTask SetAsync<T>(string key, T value, TimeSpan expiration)
    {
        memoryCache.Set(key, value, expiration);
        return ValueTask.CompletedTask;
    }
}
