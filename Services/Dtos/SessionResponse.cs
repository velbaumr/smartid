namespace Services.Dtos;

public class SessionResponse
{
    public string? State { get; set; }
    public Result? Result { get; set; }

    public Signature? Signature { get; set; }

    public Cert? Cert { get; set; }

    public string? InteractionFlowUsed { get; set; }
}