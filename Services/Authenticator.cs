using System.Text.Json;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class Authenticator(ISmartIdClient handler, ILogger logger)
{
    public async Task<string?> Authenticate(AuthenticationRequest request, string documentNumber)
    {

        var response = await handler.SendAuthenticationRequest(request, documentNumber);

        var content = await response.Content.ReadAsStringAsync();
        
        var sessionId = JsonSerializer.Deserialize<AuthenticationResponse>(content)?.SessionId;

        return await PollAuthenticationResult(sessionId);
    }

    private async Task<string?> PollAuthenticationResult(string? sessionId)
    {
        while (true)
        {
            var response = await handler.SendSessionRequest(sessionId);
            
            var content = await response.Content.ReadAsStringAsync();

            var authResult = JsonSerializer.Deserialize<SessionResponse>(content);

            if (authResult?.State == "Completed")
            {
                return authResult.Result?.EndResult;
            }
            
            await Task.Delay(5000);
        }

    }

}