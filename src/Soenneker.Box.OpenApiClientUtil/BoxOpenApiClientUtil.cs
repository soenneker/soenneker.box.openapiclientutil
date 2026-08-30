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

public sealed class BoxOpenApiClientUtil : IBoxOpenApiClientUtil
{
    private readonly AsyncSingleton<ClientState> _client;

    public BoxOpenApiClientUtil(IBoxOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<ClientState>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Box:ApiKey");
            string authHeaderName = configuration["Box:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Box:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(
                new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

            return new ClientState(new BoxOpenApiClient(requestAdapter), requestAdapter);
        });
    }

    public async ValueTask<BoxOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        ClientState state = await _client.Get(cancellationToken).NoSync();
        return state.Client;
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }

    private sealed class ClientState : IDisposable
    {
        private readonly HttpClientRequestAdapter _requestAdapter;

        public BoxOpenApiClient Client { get; }

        public ClientState(BoxOpenApiClient client, HttpClientRequestAdapter requestAdapter)
        {
            Client = client;
            _requestAdapter = requestAdapter;
        }

        public void Dispose()
        {
            _requestAdapter.Dispose();
        }
    }
}
