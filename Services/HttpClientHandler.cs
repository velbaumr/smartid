namespace Services;

public class HttpClientHandler : IHttpClientHandler
{
    private readonly HttpClient _client = new();

 
    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        return await _client.GetAsync(url);
    }
}