using Newtonsoft.Json;
using Services;
using Services.Dtos;

namespace Tests.UnitTests;

public class RequestBuilderTests: TestBase
{
    private readonly RequestBuilder _builder = new(_configuration);

    [Fact]
    public void CreatesAuthenticationRequest()
    {
        var result = _builder.Build();

        Assert.NotNull(result.Hash);
        Assert.Equal("00000000-0000-0000-0000-000000000000", result.RelyingPartyUuid);
        Assert.Equal("DEMO", result.RelyingPartyName);
    }
}