using Microsoft.Extensions.Logging;

namespace SmartId;

public class Application(ILogger<Application> logger)
{
    public async Task Run()
    {
        await Task.Delay(5000);
        logger.LogInformation("Smart ID is running");
    }
}