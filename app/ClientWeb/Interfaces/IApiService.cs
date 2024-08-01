namespace Client.Interfaces;

public interface IApiService
{
    Task<HttpResponseMessage> HttpGetRequest(string action);
    Task<HttpResponseMessage> HttpPostRequest(string action, HttpContent content);
    Task<HttpResponseMessage> HttpPutRequest(string action, HttpContent content);
    Task<HttpResponseMessage> HttpDeleteRequest(string action, HttpContent content);
}
