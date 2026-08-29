[![](https://img.shields.io/nuget/v/soenneker.box.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.box.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.box.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.box.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.openapiclientutil/)

# Soenneker.Box.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Box.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Box.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddBoxOpenApiClientUtilAsSingleton();
```

Adds `BoxOpenApiClientUtil` as a singleton service.

## What you get

- `IBoxOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `BoxOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `BoxOpenApiClientUtilRegistrar.AddBoxOpenApiClientUtilAsSingleton(services)` | Adds `BoxOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `BoxOpenApiClientUtilRegistrar.AddBoxOpenApiClientUtilAsScoped(services)` | Adds `BoxOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
