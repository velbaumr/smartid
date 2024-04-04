namespace Services.Interfaces;

public interface IHttpClientHandler
{
    Task<HttpResponseMessage> GetAsync(string url);

    Task<HttpResponseMessage> PostAsync(string url, HttpContent? content);
}