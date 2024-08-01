using Client.Helpers;
using Client.Interfaces;
using Client.Models;
using System.Text.Json;

namespace Client.Services;

public class TareasService : ITareasService
{
    private readonly IApiService api;
    private readonly ILogger logger;

    public TareasService(IApiService _api, ILogger<TareasService> _logger)
    {
        api = _api;
        logger = _logger;
    }

    public async Task<IEnumerable<TareasModel>> GetAllAsync(int metaId)
    {
        try
        {
            using (var httpResponse = await api.HttpGetRequest($"/Tareas/GetAll?metaId={metaId}"))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<List<TareasModel>>(streamResponse, ApiUtils.jsonOptions);

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

    public async Task<TareasModel> GetByIdAsync(int id, int metaId)
    {
        try
        {
            using (var httpResponse = await api.HttpGetRequest($"/Tareas/GetById?tareaId={id}&metaId={metaId}"))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<TareasModel>(streamResponse, ApiUtils.jsonOptions);

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

    public async Task<TareasModel> AddAsync(TareasModel data)
    {
        try
        {
            using (var httpResponse = await api.HttpPostRequest($"/Tareas/Add", ApiUtils.GetBodyContent(data)))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<TareasModel>(streamResponse, ApiUtils.jsonOptions);

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

    public async Task<TareasModel> UpdateAsync(TareasModel data)
    {
        try
        {
            using (var httpResponse = await api.HttpPostRequest($"/Tareas/Update", ApiUtils.GetBodyContent(data)))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<TareasModel>(streamResponse, ApiUtils.jsonOptions);

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

    public async Task<TareasModel> DeleteAsync(TareasModel data)
    {
        try
        {
            using (var httpResponse = await api.HttpPostRequest($"/Tareas/Delete", ApiUtils.GetBodyContent(data)))
            {
                var streamResponse = await httpResponse.Content.ReadAsStreamAsync();
                if (httpResponse.IsSuccessStatusCode)
                    return await JsonSerializer.DeserializeAsync<TareasModel>(streamResponse, ApiUtils.jsonOptions);

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

