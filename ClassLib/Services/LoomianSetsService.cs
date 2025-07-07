using System.Net.Http.Headers;
using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian set-related operations against an API.
/// </summary>
public class LoomianSetsService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : ILoomianSetsService
{
    /// <inheritdoc />
    public async Task<IEnumerable<LoomianSetResponseDto>?> GetAllAsync(LoomianSetQueryParams? queryParams = null)
    {
        // Create a new request to the API.
        string queryString = string.Empty;
        if (queryParams != null)
        {
            queryString = queryParams.ToQueryString();
        }
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianSets{queryString}");
        response.EnsureSuccessStatusCode();
        IEnumerable<LoomianSetResponseDto>? items = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianSetResponseDto>>();
        return items;
    }

    /// <inheritdoc />
    public async Task<LoomianSetResponseDto?> GetByIdAsync(Guid id, LoomianSetQueryParams? queryParams = null)
    {
        // Create a new request to the API.
        string queryString = string.Empty;
        if (queryParams != null)
        {
            queryString = queryParams.ToQueryString();
        }
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianSets/{id}{queryString}");
        response.EnsureSuccessStatusCode();
        LoomianSetResponseDto? item = await response.Content.ReadFromJsonAsync<LoomianSetResponseDto>();
        return item;
    }

    /// <inheritdoc />
    public async Task<LoomianSetResponseDto> SubmitSetAsync(SubmitLoomianSetRequestDto loomianSet, string authToken)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{apiSettings.Value.BaseUrl}/loomianSets/submit", loomianSet);
        response.EnsureSuccessStatusCode();
        LoomianSetResponseDto? item = await response.Content.ReadFromJsonAsync<LoomianSetResponseDto>();
        if (item is null)
        {
            throw new InvalidOperationException("Failed to deserialize the LoomianSet.");
        }
        return item;
    }

    /// <inheritdoc />
    public async Task RateLoomianSetAsync(Guid loomianSetId, int starRating, string authToken)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{apiSettings.Value.BaseUrl}/loomianSets/{loomianSetId}/rate", starRating);
        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task<StarRatingResponseDto?> GetMyRatingAsync(Guid loomianSetId, string authToken)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianSets/{loomianSetId}/myRating");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null; // No rating found for the user.
        }
        response.EnsureSuccessStatusCode();
        StarRatingResponseDto? rating = await response.Content.ReadFromJsonAsync<StarRatingResponseDto>();
        return rating;
    }
}