using Services.Dtos;

namespace Services.Interfaces;

public interface ISmartIdClient
{
    Task<HttpResponseMessage> SendAuthenticationRequest(AuthenticationRequest request, string documentNumber);

    Task<HttpResponseMessage> SendSessionRequest(string? sessionId);
}