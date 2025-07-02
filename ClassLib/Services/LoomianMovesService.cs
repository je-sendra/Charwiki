using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
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
    public async Task<IEnumerable<LoomianMoveResponseDto>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianMoves");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        IEnumerable<LoomianMoveResponseDto>? moves = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianMoveResponseDto>>();
        return moves ?? [];
    }
}