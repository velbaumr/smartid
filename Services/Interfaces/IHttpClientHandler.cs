namespace Services.Interfaces;

public interface IHttpClientHandler
{
    Task<HttpResponseMessage> GetAsync(string url);
}