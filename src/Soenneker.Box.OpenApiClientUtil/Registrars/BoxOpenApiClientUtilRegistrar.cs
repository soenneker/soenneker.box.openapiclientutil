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
    /// Adds <see cref="BoxOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddBoxOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddBoxOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IBoxOpenApiClientUtil, BoxOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="BoxOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddBoxOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddBoxOpenApiHttpClientAsSingleton()
                .TryAddScoped<IBoxOpenApiClientUtil, BoxOpenApiClientUtil>();

        return services;
    }
}
