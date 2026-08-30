[![](https://img.shields.io/nuget/v/soenneker.box.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.box.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.box.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.box.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.box.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.box.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Box.OpenApiClientUtil

Provides an authenticated, cached instance of the Kiota-generated Box API client.

## Installation

```bash
dotnet add package Soenneker.Box.OpenApiClientUtil
```

## Configuration

```json
{
  "Box": {
    "ApiKey": "<Box access token>",
    "ClientBaseUrl": "https://api.box.com/2.0",
    "AuthHeaderName": "Authorization",
    "AuthHeaderValueTemplate": "Bearer {token}"
  }
}
```

Only `Box:ApiKey` is required. The other values show their defaults. `{token}` in the header template is replaced with the configured token.

Store the token in a secret provider rather than source control or a checked-in settings file.

## Registration

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Box.OpenApiClientUtil.Registrars;

services.AddBoxOpenApiClientUtilAsSingleton();
```

Use `AddBoxOpenApiClientUtilAsScoped()` when each dependency-injection scope should own an isolated generated client, request adapter, and HTTP-client cache entry.

## Usage

```csharp
using Soenneker.Box.OpenApiClient.Models;
using Soenneker.Box.OpenApiClientUtil.Abstract;

public sealed class BoxProfileService
{
    private readonly IBoxOpenApiClientUtil _boxClients;

    public BoxProfileService(IBoxOpenApiClientUtil boxClients)
    {
        _boxClients = boxClients;
    }

    public async ValueTask<UserFull?> GetCurrentUser(
        CancellationToken cancellationToken)
    {
        var box = await _boxClients.Get(cancellationToken);

        return await box.Users.Me.GetAsync(
            request =>
            {
                request.QueryParameters.Fields = ["id", "name", "login"];
            },
            cancellationToken);
    }
}
```

Access other resources through the generated request-builder hierarchy, such as `box.Files[fileId]`, `box.Folders[folderId]`, `box.Search`, and `box.Events`.

## Lifecycle and behavior

- The first `Get` creates the HTTP client, Kiota request adapter, and generated client. Later calls on the same utility return that client.
- Configuration is captured during first initialization. Rotate a token by replacing the owning dependency-injection scope or application instance.
- The token passed to `Get` cancels initialization only. Pass a cancellation token to each generated endpoint call as well.
- Let dependency injection dispose `IBoxOpenApiClientUtil`. Disposal releases its request adapter and, for scoped registration, its isolated HTTP-client entry.
- Generated endpoint methods can return `null` when the Box schema permits an empty response.
- Service errors are surfaced through generated error models or Kiota exceptions according to the endpoint mapping.
- The generated client follows Box's OpenAPI description, so its public request builders and models can change when the client package is regenerated.
