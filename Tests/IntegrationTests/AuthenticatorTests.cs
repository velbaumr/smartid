using Microsoft.Extensions.Logging;
using Moq;
using Services;

namespace Tests.IntegrationTests;

public class AuthenticatorTests: TestBase
{
    private readonly Authenticator _authenticator;
    public AuthenticatorTests()
    {
        var logger = new Mock<ILogger>();
        var handler = new SmartIdClient(_configuration, logger.Object);
        _authenticator = new Authenticator(handler, logger.Object);
    }

    [Fact]
    public async Task CompletesAuthentication()
    {
        var request = new RequestBuilder(_configuration).Build();
        var result = await _authenticator.Authenticate(request, "PNOEE-50001029996-MOCK-Q");

        Assert.Equal("OK", result.Value);
    }
}