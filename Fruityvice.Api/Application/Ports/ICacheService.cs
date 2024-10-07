using Microsoft.Extensions.Caching.Distributed;

namespace Fruityvice.Api.Application.Ports;

public interface ICacheService
{
    ValueTask SetAsync<T>(string key, T value, TimeSpan expiration);

    ValueTask InvalidateAsync(string key);

    ValueTask<T> GetAsync<T>(string key);
}
