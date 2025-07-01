using System.Net.Http.Headers;
using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian set-related operations against an API.
/// </summary>
public class LoomianSetsService : CrudControllerServiceTemplate<LoomianSet>, ILoomianSetsService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<ApiSettings> _apiSettings;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="apiSettings"></param>
    public LoomianSetsService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : base(httpClient, apiSettings, "loomianSets")
    {
        _httpClient = httpClient;
        _apiSettings = apiSettings;
    }

    /// <inheritdoc />
    public async Task<LoomianSet> GetByIdAsync(Guid id, bool includeValueToStatAssignments = false, bool includeRatings = false)
    {
        // Create a new request to the API.
        HttpResponseMessage response = await _httpClient.GetAsync($"{_apiSettings.Value.BaseUrl}/loomianSets/{id}?"
            + $"includeValueToStatAssignments={includeValueToStatAssignments}"
            + $"&includeRatings={includeRatings}");
        response.EnsureSuccessStatusCode();
        var item = await response.Content.ReadFromJsonAsync<LoomianSet>();
        if (item is null)
        {
            throw new InvalidOperationException("Failed to deserialize the LoomianSet.");
        }
        return item;
    }

    /// <inheritdoc />
    public async Task<LoomianSet> SubmitSetAsync(LoomianSetRequestDto loomianSet, string authToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_apiSettings.Value.BaseUrl}/loomianSets", loomianSet);
        response.EnsureSuccessStatusCode();
        LoomianSet? item = await response.Content.ReadFromJsonAsync<LoomianSet>();
        if (item is null)
        {
            throw new InvalidOperationException("Failed to deserialize the LoomianSet.");
        }
        return item;
    }

    /// <inheritdoc />
    public async Task RateLoomianSetAsync(Guid loomianSetId, int starRating, string authToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_apiSettings.Value.BaseUrl}/loomianSets/{loomianSetId}/rate", starRating);
        response.EnsureSuccessStatusCode();
    }
}