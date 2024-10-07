namespace Fruityvice.Api.ExceptionHandler;

public static class Extensions
{
    public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        return services;
    }

}