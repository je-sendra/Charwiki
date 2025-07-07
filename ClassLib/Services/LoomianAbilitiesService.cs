using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian ability-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class LoomianAbilitiesService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : ILoomianAbilitiesService
{
    /// <inheritdoc/>
    public async Task<OperationResultWithReturnData<IEnumerable<LoomianAbilityResponseDto>>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianAbilities");
        if (!response.IsSuccessStatusCode)
        {
            return new()
            {
                HasFailed = true,
                UserMessage = "Failed to retrieve Loomian abilities.",
                InternalMessage = response.ReasonPhrase ?? "Unknown error",
            };
        }
        IEnumerable<LoomianAbilityResponseDto>? abilities = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianAbilityResponseDto>>();
        return new OperationResultWithReturnData<IEnumerable<LoomianAbilityResponseDto>>()
        {
            HasFailed = false,
            UserMessage = "Loomian abilities retrieved successfully.",
            InternalMessage = "Loomian abilities retrieved successfully.",
            ReturnData = abilities ?? [],
        };
    }
}