using System.Text.Json;
using Microsoft.Extensions.Logging;
using Moq;
using Services;
using Services.Dtos;

namespace Tests.IntegrationTests;

public class SmartIdClientTests : TestBase
{
    private readonly SmartIdClient _client;
    private readonly JsonSerializerOptions _options;

    public SmartIdClientTests()
    {
        var logger = new Mock<ILogger<SmartIdClient>>();
        _client = new SmartIdClient(Configuration, logger.Object);
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetsSessionId()
    {
        var result = await GetSessionResponse("PNOEE-50001029996-MOCK-Q");
        var sessionId = await GetSessionId(result);

        Assert.NotNull(result);
        Assert.Equal(36, sessionId?.Length);
    }

    [Theory]
    [InlineData("PNOEE-50001029996-MOCK-Q", "OK")]
    [InlineData("PNOEE-30403039983-MOCK-Q", "TIMEOUT")]
    [InlineData("PNOEE-30403039917-MOCK-Q", "USER_REFUSED")]
    [InlineData("PNOEE-30403039928-MOCK-Q", "USER_REFUSED_DISPLAYTEXTANDPIN")]
    public async Task GetsAuthenticationResponse(string documentNumber, string responseState)
    {
        var sessionResult = await GetSessionResponse(documentNumber);
        var sessionId = await GetSessionId(sessionResult);

        var result = await _client.SendSessionRequest(sessionId);
        var endResult = await ExtractEndResult(result);

        Assert.Equal(responseState, endResult);
    }

    private async Task<HttpResponseMessage> GetSessionResponse(string documentNumber)
    {
        var builder = new RequestBuilder(Configuration);
        var request = builder.Build();
        var result = await _client.SendAuthenticationRequest(request, documentNumber);
        return result;
    }

    private async Task<string?> GetSessionId(HttpResponseMessage message)
    {
        var content = await message.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<AuthenticationResponse>(content, _options)?.SessionId;
    }

    private async Task<string?> ExtractEndResult(HttpResponseMessage message)
    {
        var content = await message.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<SessionResponse>(content, _options)?.Result?.EndResult;
    }
}