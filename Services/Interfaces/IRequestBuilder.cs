using Services.Dtos;

namespace Services.Interfaces;

public interface IRequestBuilder
{
    string? VerificationCode { get; }

    AuthenticationRequest Build();
}