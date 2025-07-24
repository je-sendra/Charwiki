using System.Net.Http.Headers;
using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
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

    /// <inheritdoc />
    public async Task<OperationResult> ApproveSetAsync(Guid loomianSetId, string authToken)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        HttpResponseMessage response = await httpClient.PostAsync($"{apiSettings.Value.BaseUrl}/loomianSets/{loomianSetId}/approve", null);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new OperationResult
            {
                HasFailed = true,
                UserMessage = "Loomian set not found.",
                InternalMessage = "The specified Loomian set ID does not exist.",
            };
        }

        if (!response.IsSuccessStatusCode)
        {
            return new OperationResult
            {
                HasFailed = true,
                UserMessage = "Failed to approve Loomian set.",
                InternalMessage = response.ReasonPhrase ?? "Unknown error",
            };
        }
        response.EnsureSuccessStatusCode();
        return new OperationResult
        {
            HasFailed = false,
            UserMessage = "Loomian set approved successfully.",
            InternalMessage = "The Loomian set has been approved.",
        };
    }
}