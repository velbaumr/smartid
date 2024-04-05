using Services.Dtos;

namespace Services.Interfaces;

public interface IAuthenticator
{
    Task<Result<string>> Authenticate(AuthenticationRequest request,
        string documentNumber);
}