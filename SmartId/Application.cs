using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace SmartId;

public class Application(ILogger<Application> logger, IAuthenticator authenticator, IRequestBuilder requestBuilder)
{
    public async Task Run()
    {
        logger.LogInformation("Smart ID is running");

        var documentNumber = "PNOEE-30403039928-MOCK-Q";
        var request = requestBuilder.Build();
        var result = await authenticator.Authenticate(request, documentNumber);
        
        logger.LogInformation(result.Value ?? result.ErrorMessage);
    }
}