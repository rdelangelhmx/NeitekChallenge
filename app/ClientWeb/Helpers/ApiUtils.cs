using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace Client.Helpers;

public static class ApiUtils
{
    public static JsonSerializerOptions jsonOptions { get; } = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNameCaseInsensitive = true, ReferenceHandler = ReferenceHandler.IgnoreCycles };
    public static FormUrlEncodedContent GetUrlEncodedContent(this object content) => new FormUrlEncodedContent(System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(content, jsonOptions), jsonOptions));
    public static StringContent GetBodyContent(this object content) => new StringContent(System.Text.Json.JsonSerializer.Serialize(content, jsonOptions), Encoding.UTF8, "application/json");
}
