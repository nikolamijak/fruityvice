using Fruityvice.Api.Application.Ports;

namespace Fruityvice.Api.Infrastructure.Persistance;

public static class Extensions
{
    public static IServiceCollection AddInMemoryMetadataRepository(this IServiceCollection services)
    {
        services.AddSingleton<IFruitMetadataRepository, InMemoryMetadataRepository> ();
        return services;
    }

}
