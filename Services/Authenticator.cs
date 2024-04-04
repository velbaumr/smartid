using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;

namespace Services;

public class Authenticator(IHttpClientHandler handler, IConfiguration configuration, ILogger logger)
{
    public string Authenticate(AuthenticationRequest request, string idNumber)
    {
        throw new NotImplementedException();
    }
}