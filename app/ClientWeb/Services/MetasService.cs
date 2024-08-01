using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using System.Text.Json;

namespace Client.Services;

public class MetasService : IMetasService
{
    private readonly IApiService api;
    private readonly ILogger logger;

    public MetasService(IApiService _api, ILogger<MetasService> _logger)
    {
        api = _api;
        logger = _logger;
    }

    public async Task<IEnumerable<MetasModel>> GetAllAsync()
    {
        try
        {
            using (var httpResponse = await api.HttpGetRequest($"/Metas/GetAll/"))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<List<MetasModel>>(streamResponse, ApiUtils.jsonOptions);

                var response = httpResponse.RequestMessage.ToString();
                throw new Exception($"Content: {response}, ReasonPhrase: {httpResponse.ReasonPhrase}, StatusCode: {(int)httpResponse.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{GetType().Assembly.GetName()} => {GetType().Name}");
            return null;
        }
    }

    public async Task<MetasModel> GetByIdAsync(int id)
    {
        try
        {
            using (var httpResponse = await api.HttpGetRequest($"/Metas/GetById/{id}"))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<MetasModel>(streamResponse, ApiUtils.jsonOptions);

                var response = httpResponse.RequestMessage.ToString();
                throw new Exception($"Content: {response}, ReasonPhrase: {httpResponse.ReasonPhrase}, StatusCode: {(int)httpResponse.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{GetType().Assembly.GetName()} => {GetType().Name}");
            return null;
        }
    }

    public async Task<MetasModel> AddAsync(MetasModel data)
    {
        try
        {
            using (var httpResponse = await api.HttpPostRequest($"/Metas/Add", ApiUtils.GetBodyContent(data)))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<MetasModel>(streamResponse, ApiUtils.jsonOptions);

                var response = httpResponse.RequestMessage.ToString();
                throw new Exception($"Content: {response}, ReasonPhrase: {httpResponse.ReasonPhrase}, StatusCode: {(int)httpResponse.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{GetType().Assembly.GetName()} => {GetType().Name}");
            return null;
        }
    }

    public async Task<MetasModel> UpdateAsync(MetasModel data)
    {
        try
        {
            using (var httpResponse = await api.HttpPutRequest($"/Metas/Update", ApiUtils.GetBodyContent(data)))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<MetasModel>(streamResponse, ApiUtils.jsonOptions);

                var response = httpResponse.RequestMessage.ToString();
                throw new Exception($"Content: {response}, ReasonPhrase: {httpResponse.ReasonPhrase}, StatusCode: {(int)httpResponse.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{GetType().Assembly.GetName()} => {GetType().Name}");
            return null;
        }
    }

    public async Task<MetasModel> DeleteAsync(MetasModel data)
    {
        try
        {
            using (var httpResponse = await api.HttpDeleteRequest($"/Metas/Delete", ApiUtils.GetBodyContent(data)))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<MetasModel>(streamResponse, ApiUtils.jsonOptions);

                var response = httpResponse.RequestMessage.ToString();
                throw new Exception($"Content: {response}, ReasonPhrase: {httpResponse.ReasonPhrase}, StatusCode: {(int)httpResponse.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{GetType().Assembly.GetName()} => {GetType().Name}");
            return null;
        }
    }
}
