namespace Services.Dtos;

public class AuthenticationRequest
{
    public string? RelyingPartyUuid { get; set; }
    
    public string? RelyingPartyName { get; set; }
    
    public string? CertificateLevel { get; set; }
    
    public string? Hash { get; set; }
    
    public string? HashType { get; set; }

    public List<AllowedInteraction> AllowedInteractionsOrder { get; set; } = [];
}