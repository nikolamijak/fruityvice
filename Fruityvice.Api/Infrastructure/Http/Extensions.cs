using Fruityvice.Api.Application.Ports;
using Fruityvice.Api.ExceptionHandler;

namespace Fruityvice.Api.Infrastructure.Http;

public static class Extensions
{
    public static IServiceCollection AddFruityViceHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IFruitApiService, FruitApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://www.fruityvice.com");
        });
        return services;
    }

}
