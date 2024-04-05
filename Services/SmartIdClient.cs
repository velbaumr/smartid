using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class SmartIdClient(IConfiguration configuration, ILogger logger) : ISmartIdClient
{
    private readonly IConfiguration _configuration = configuration.GetSection("smartId");

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<HttpResponseMessage> SendAuthenticationRequest(AuthenticationRequest request,
        string documentNumber)
    {
        var serialized = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(serialized, Encoding.UTF8, "application/json");
        var baseUrl = _configuration["baseUrl"] ??
                      throw new InvalidOperationException("Can't read base url value from settings");
        var client = new HttpClient();
        var url = $"{baseUrl}authentication/document/{documentNumber}";

        try
        {
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<HttpResponseMessage> SendSessionRequest(string? sessionId)
    {
        var baseUrl = _configuration["baseUrl"] ??
                      throw new InvalidOperationException("Can't read base url value from settings");
        var url = $"{baseUrl}session/{sessionId}";
        var client = new HttpClient();

        try
        {
            var response = await client.GetAsync(url);
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