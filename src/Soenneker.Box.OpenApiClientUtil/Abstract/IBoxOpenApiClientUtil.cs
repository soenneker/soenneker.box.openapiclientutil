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
    ValueTask<BoxOpenApiClient> Get(CancellationToken cancellationToken = default);
}
