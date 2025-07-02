using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
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
    public async Task<IEnumerable<LoomianAbilityResponseDto>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianAbilities");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        IEnumerable<LoomianAbilityResponseDto>? abilities = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianAbilityResponseDto>>();
        return abilities ?? [];
    }
}