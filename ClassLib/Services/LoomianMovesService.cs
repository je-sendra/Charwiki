using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian move-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class LoomianMovesService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : ILoomianMovesService
{
    /// <inheritdoc/>
    public async Task<OperationResultWithReturnData<IEnumerable<LoomianMoveResponseDto>>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianMoves");
        if (!response.IsSuccessStatusCode)
        {
            return new()
            {
                HasFailed = true,
                UserMessage = "Failed to retrieve Loomian moves.",
                InternalMessage = response.ReasonPhrase ?? "Unknown error",
            };
        }
        IEnumerable<LoomianMoveResponseDto>? moves = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianMoveResponseDto>>();
        return new()
        {
            HasFailed = false,
            ReturnData = moves ?? [],
            UserMessage = "Successfully retrieved Loomian moves.",
            InternalMessage = "Moves retrieved successfully.",
        };
    }
}