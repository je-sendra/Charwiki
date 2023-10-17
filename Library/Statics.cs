using System.Text.Json;

namespace VewTech.Charwiki.Library;

/// <summary>
/// Static properties to be used accross the application.
/// </summary>
public static class Statics
{
    /// <summary>
    /// The client for the Charwiki API.
    /// </summary>
    public static HttpClient ApiClient { get; set; } = new()
    {
        BaseAddress = new Uri("http://localhost:5000")
    };

    /// <summary>
    /// The JSON serializer options.
    /// </summary>
    public static JsonSerializerOptions JsonSerializerOptions { get; set; }
     = new(JsonSerializerDefaults.Web) { };
}