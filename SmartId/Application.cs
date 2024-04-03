
using Microsoft.Extensions.Logging;

namespace SmartId;

public class Application(ILogger<Application> logger)
{
    public void Run()
    {
        logger.LogInformation("Smart ID is running");
    }
}