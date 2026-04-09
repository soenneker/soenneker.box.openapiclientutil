using Soenneker.Box.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Box.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class BoxOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IBoxOpenApiClientUtil _openapiclientutil;

    public BoxOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IBoxOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
