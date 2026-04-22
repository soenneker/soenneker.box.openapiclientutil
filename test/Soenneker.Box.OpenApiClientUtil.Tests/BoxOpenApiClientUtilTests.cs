using Soenneker.Box.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Box.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class BoxOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IBoxOpenApiClientUtil _openapiclientutil;

    public BoxOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IBoxOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
