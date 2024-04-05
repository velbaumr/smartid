using System.Text.Json;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class Authenticator(ISmartIdClient handler, ILogger logger)
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    public async Task<Result<string>> Authenticate(AuthenticationRequest request, string documentNumber)
    {
        try
        {
            var response = await handler.SendAuthenticationRequest(request, documentNumber);

            var content = await response.Content.ReadAsStringAsync();
        
            var sessionId = JsonSerializer.Deserialize<AuthenticationResponse>(content, _options)?.SessionId;

            var result = await PollAuthenticationResult(sessionId!);

            return new Result<string> { Value = result };
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return new Result<string> { ErrorMessage = e.Message };
        }
    }

    private async Task<string?> PollAuthenticationResult(string sessionId)
    {
        ArgumentNullException.ThrowIfNull(sessionId);
        
        var authResult = new SessionResponse();

        while (authResult?.State != "COMPLETE")
        {
            var response = await handler.SendSessionRequest(sessionId);

            var content = await response.Content.ReadAsStringAsync();

            authResult = JsonSerializer.Deserialize<SessionResponse>(content, _options);

            if (authResult?.State == "COMPLETE")
            {
                return authResult.Result?.EndResult;
            }

            await Task.Delay(5000);
        }

        return null;
    }
}