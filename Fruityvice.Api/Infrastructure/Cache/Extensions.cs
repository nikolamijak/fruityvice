using Fruityvice.Api.Application.Ports;
using Fruityvice.Api.Infrastructure.Persistance;

namespace Fruityvice.Api.Infrastructure.Cache;

public static class Extensions
{
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, InMemoryCacheService> ();
        return services;
    }

}
