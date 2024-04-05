using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace SmartId;

public class Application(ILogger<Application> logger, IAuthenticator authenticator)
{
    public async Task Run()
    {
        await Task.Delay(5000);
        logger.LogInformation("Smart ID is running");
    }
}