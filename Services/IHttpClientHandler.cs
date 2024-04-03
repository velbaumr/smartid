namespace Services
{
    public interface IHttpClientHandler
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}