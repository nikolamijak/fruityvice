using FluentValidation;
using MediatR;
using System.Reflection;

namespace Fruityvice.Api.Application.Decorators;

public static class Extensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        return services;
    }
    public static IServiceCollection RegisterAllValidators(this IServiceCollection services)
    {
        var iValidatorImplementations = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetLoadableTypes())
        .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))
        && !p.IsAbstract && !p.IsInterface && !p.IsNestedPrivate && p.IsClass && !p.FullName!.ToLowerInvariant().StartsWith("fluentvalidation", StringComparison.InvariantCultureIgnoreCase));

        foreach (var impl in iValidatorImplementations)
        {
            var serviceType = impl.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IValidator<>));
            services.AddTransient(serviceType, impl);
        }

        return services;
    }

    public static IServiceCollection RegisterCachePolicies(this IServiceCollection services)
    {
        services.RegisterImplementations(typeof(ICachePolicy<,>));
        return services;
    }

    public static IServiceCollection AddValidationDecorator(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDecorator<,>));

        return services;
    }

    public static IServiceCollection AddInMemoryCacheDecorator(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(InMemoryCacheDecorator<,>));

        return services;
    }

    private static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
    {
        // TODO: Argument validation
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e?.Types?.Where(t => t != null)!;
        }
    }

    private static void RegisterImplementations(this IServiceCollection services, Type type)
    {
        // Get All Implementations of type
        var IServiceImplementation = AppDomain.CurrentDomain.GetAssemblies()
          .SelectMany(s => s.GetTypes())
          .Where(p => p.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)
                  && !p.IsAbstract && !p.IsInterface);

        foreach (var impl in IServiceImplementation)
        {
            var service = impl.GetInterfaces().First(i => i.GetGenericTypeDefinition() == type);
            services.AddScoped(service, impl);
        }
    }
}
