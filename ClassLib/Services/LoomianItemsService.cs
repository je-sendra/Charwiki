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
    public async Task<IEnumerable<LoomianItemResponseDto>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomianItems");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        IEnumerable<LoomianItemResponseDto>? items = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianItemResponseDto>>();
        return items ?? [];
    }
}