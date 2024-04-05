using Microsoft.Extensions.Configuration;
using Services.Dtos;

namespace Services;

public class RequestBuilder(IConfiguration configuration)
{
    public string? VerificationCode { get; private set; }

    public AuthenticationRequest Build()
    {
        var hash = HashCreator.CreateHash().ToArray();
        var settings = configuration.GetSection("smartId");

        var request = new AuthenticationRequest
        {
            HashType = "SHA256",
            Hash = Convert.ToBase64String(hash),
            RelyingPartyUUID = settings["uuid"],
            RelyingPartyName = settings["name"],
            CertificateLevel = "QUALIFIED",
            AllowedInteractionsOrder =
                [new AllowedInteraction { DisplayText60 = settings["displayText"], Type = "verificationCodeChoice" }]
        };

        VerificationCode = CodeCalculator.CalculateCode(hash);

        return request;
    }
}