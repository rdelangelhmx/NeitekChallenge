using Client.Classes;
using Client.Interfaces;
using System.Net;

namespace Client.Services;

public class ApiService : IApiService
{
    private readonly HttpClient httpClient;
    private readonly ILogger logger;
    private readonly ConfigApp configApp;

    HttpResponseMessage response { get; set; }

    public ApiService(IHttpClientFactory _httpClient, ILogger<ApiService> _logger, ConfigApp _configApp)
    {
        configApp = _configApp;
        httpClient = _httpClient.CreateClient(_configApp.Api.Client);
        logger = _logger;
    }

    public async Task<HttpResponseMessage> HttpGetRequest(string action)
    {
        try
        {
            response = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{configApp.Api.Url}{action}")
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Exception => HttpGetRequest {action}");
            response.Content = new StringContent(ex.Message);
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
        return response;
    }

    public async Task<HttpResponseMessage> HttpPostRequest(string action, HttpContent content)
    {
        try
        {
            response = await SendAsync(new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{configApp.Api.Url}{action}")
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Exception => HttpPostRequest {action}");
            response.Content = new StringContent(ex.Message);
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
        return response;
    }

    public async Task<HttpResponseMessage> HttpPutRequest(string action, HttpContent content)
    {
        try
        {
            response = await SendAsync(new HttpRequestMessage
            {
                Content = content,
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{configApp.Api.Url}{action}")
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Exception => HttpPutRequest {action}");
            response.Content = new StringContent(ex.Message);
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
        return response;
    }

    public async Task<HttpResponseMessage> HttpDeleteRequest(string action)
    {
        try
        {
            response = await SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{configApp.Api.Url}{action}")
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Exception => HttpDeleteRequest {action}");
            response.Content = new StringContent(ex.Message);
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
        return response;
    }

    #region Private Methods
    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage)
    {
        var response = await httpClient
            .SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, ConfigureTimeout(30))
            .ConfigureAwait(false);

        return response;
    }

    private static CancellationToken ConfigureTimeout(int timeout)
    {
        var tokenSource = new CancellationTokenSource();
        if (timeout > 0)
            tokenSource.CancelAfter(TimeSpan.FromSeconds(timeout));
        return tokenSource.Token;
    }
    #endregion
}
