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
}