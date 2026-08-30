using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Box.HttpClients.Registrars;
using Soenneker.Box.OpenApiClientUtil.Abstract;

namespace Soenneker.Box.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class BoxOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IBoxOpenApiClientUtil"/> and its HTTP-client provider as singleton services.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddBoxOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddBoxOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IBoxOpenApiClientUtil, BoxOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IBoxOpenApiClientUtil"/> and its HTTP-client provider as scoped services.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddBoxOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddBoxOpenApiHttpClientAsScoped()
                .TryAddScoped<IBoxOpenApiClientUtil, BoxOpenApiClientUtil>();

        return services;
    }
}
