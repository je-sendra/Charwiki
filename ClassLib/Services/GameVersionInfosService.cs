using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for game version info-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class GameVersionInfosService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : IGameVersionInfosService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<GameVersionInfoResponseDto>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/gameVersionInfos");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        IEnumerable<GameVersionInfoResponseDto>? gameVersionInfos = await response.Content.ReadFromJsonAsync<IEnumerable<GameVersionInfoResponseDto>>();
        return gameVersionInfos ?? [];
    }
}