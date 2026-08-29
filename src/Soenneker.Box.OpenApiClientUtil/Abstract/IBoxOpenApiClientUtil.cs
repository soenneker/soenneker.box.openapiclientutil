using Soenneker.Box.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Box.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IBoxOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured box Open API Client used by the box open api client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested box Open API Client.</returns>
    ValueTask<BoxOpenApiClient> Get(CancellationToken cancellationToken = default);
}
