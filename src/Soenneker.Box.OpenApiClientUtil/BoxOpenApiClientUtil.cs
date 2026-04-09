using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Box.HttpClients.Abstract;
using Soenneker.Box.OpenApiClientUtil.Abstract;
using Soenneker.Box.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Box.OpenApiClientUtil;

///<inheritdoc cref="IBoxOpenApiClientUtil"/>
public sealed class BoxOpenApiClientUtil : IBoxOpenApiClientUtil
{
    private readonly AsyncSingleton<BoxOpenApiClient> _client;

    public BoxOpenApiClientUtil(IBoxOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<BoxOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Box:ApiKey");
            string authHeaderValueTemplate = configuration["Box:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new BoxOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<BoxOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
