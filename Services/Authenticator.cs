using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class Authenticator(IHttpClientHandler handler, IConfiguration configuration, ILogger logger)
{
    private readonly IConfiguration _configuration = configuration.GetSection("smartId");
    public async Task<string?> Authenticate(AuthenticationRequest request, string documentNumber)
    {
        var response = await SendAuthenticationRequest(request, documentNumber);

        var content = await response.Content.ReadAsStringAsync();
        
        var sessionId = JsonSerializer.Deserialize<AuthenticationResponse>(content)?.SessionId;

        return await PollAuthenticationResult(sessionId);
    }

    private async Task<string?> PollAuthenticationResult(string? sessionId)
    {
        while (true)
        {
            var response = await SendSessionRequest(sessionId);
            
            var content = await response.Content.ReadAsStringAsync();

            var authResult = JsonSerializer.Deserialize<SessionResponse>(content);

            if (authResult?.State == "Completed")
            {
                return authResult.Result?.EndResult;
            }
            
            await Task.Delay(5000);
        }

    }

    private async Task<HttpResponseMessage> SendAuthenticationRequest(AuthenticationRequest request, string documentNumber)
    {
        var serialized = JsonSerializer.Serialize(request);
        var content = new StringContent(serialized);
        var baseUrl = _configuration["baseUrl"];
        
        var url = $"{baseUrl}/authentication/document/{documentNumber}";
        try
        {
            var response = await handler.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }

    private async Task<HttpResponseMessage> SendSessionRequest(string sessionId)
    {
        var baseUrl = _configuration["baseUrl"];
        var url = $"{baseUrl}session/{sessionId}";

        try
        {
            var response = await handler.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }
}