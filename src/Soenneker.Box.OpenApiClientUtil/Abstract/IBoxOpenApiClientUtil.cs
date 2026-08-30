using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Box.OpenApiClient;

namespace Soenneker.Box.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a configured, reusable Box OpenAPI client.
/// </summary>
public interface IBoxOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached generated client for this utility instance.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel client initialization.</param>
    /// <returns>An authenticated Box OpenAPI client.</returns>
    ValueTask<BoxOpenApiClient> Get(CancellationToken cancellationToken = default);
}
