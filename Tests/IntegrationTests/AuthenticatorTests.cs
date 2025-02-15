﻿using Microsoft.Extensions.Logging;
using Moq;
using Services;

namespace Tests.IntegrationTests;

public class AuthenticatorTests : TestBase
{
    private readonly Authenticator _authenticator;

    public AuthenticatorTests()
    {
        var smartIdLogger = new Mock<ILogger<SmartIdClient>>();
        var handler = new SmartIdClient(Configuration, smartIdLogger.Object);
        _authenticator = new Authenticator(handler);
    }

    [Theory]
    [InlineData("PNOEE-50001029996-MOCK-Q", "OK")]
    [InlineData("PNOEE-30403039983-MOCK-Q", "TIMEOUT")]
    [InlineData("PNOEE-30403039917-MOCK-Q", "USER_REFUSED")]
    [InlineData("PNOEE-30403039928-MOCK-Q", "USER_REFUSED_DISPLAYTEXTANDPIN")]
    public async Task CompletesAuthentication(string documentNumber, string responseState)
    {
        var request = new RequestBuilder(Configuration).Build();
        var result = await _authenticator.Authenticate(request, documentNumber);

        Assert.Equal(responseState, result.Value);
    }
}