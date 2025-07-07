using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian item-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class LoomianItemsService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : ILoomianItemsService
{
    /// <inheritdoc/>
    public async Task<OperationResultWithReturnData<IEnumerable<LoomianItemResponseDto>>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianItems");
        if (!response.IsSuccessStatusCode)
        {
            return new()
            {
                HasFailed = true,
                UserMessage = "Failed to retrieve Loomian items.",
                InternalMessage = response.ReasonPhrase ?? "Unknown error",
            };
        }
        IEnumerable<LoomianItemResponseDto>? items = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianItemResponseDto>>();
        return new()
        {
            HasFailed = false,
            ReturnData = items ?? [],
            UserMessage = "Successfully retrieved Loomian items.",
            InternalMessage = "Items retrieved successfully.",
        };
    }
}