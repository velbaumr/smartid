using System.Text.Json;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class Authenticator(
    ISmartIdClient smartIdClient): IAuthenticator
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<Result<string>> Authenticate(AuthenticationRequest request,
        string documentNumber)
    {
        try
        {
            var response = await smartIdClient.SendAuthenticationRequest(request,
                documentNumber);

            var content = await response.Content.ReadAsStringAsync();

            var sessionId = JsonSerializer.Deserialize<AuthenticationResponse>(content,
                    _options)
                ?.SessionId;

            var result = await PollAuthenticationResult(sessionId!);

            if (result == null)
            {
                return new Result<string>
                {
                    ErrorMessage = "application authentication timeout"
                };
            }
            return new Result<string>
            {
                Value = result
            };
        }
        catch (Exception e)
        {
            return new Result<string>
            {
                ErrorMessage = e.Message
            };
        }
    }

    private async Task<string?> PollAuthenticationResult(string sessionId)
    {
        ArgumentNullException.ThrowIfNull(sessionId);

        var authResult = new SessionResponse();

        while (authResult?.State != "COMPLETE")
        {
            var response = await smartIdClient.SendSessionRequest(sessionId);

            var content = await response.Content.ReadAsStringAsync();

            authResult = JsonSerializer.Deserialize<SessionResponse>(content,
                _options);

            if (authResult?.State == "COMPLETE") return authResult.Result?.EndResult;

            await Task.Delay(5000);
        }

        return null;
    }
}