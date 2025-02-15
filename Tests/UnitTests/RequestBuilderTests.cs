﻿using Services;

namespace Tests.UnitTests;

public class RequestBuilderTests : TestBase
{
    private readonly RequestBuilder _builder = new(Configuration);

    [Fact]
    public void CreatesAuthenticationRequest()
    {
        var result = _builder.Build();

        Assert.NotNull(result.Hash);
        Assert.Equal("00000000-0000-0000-0000-000000000000", result.RelyingPartyUUID);
        Assert.Equal("DEMO", result.RelyingPartyName);
    }
}