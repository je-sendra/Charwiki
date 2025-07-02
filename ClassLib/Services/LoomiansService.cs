using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian-related operations against an API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class LoomiansService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : ILoomiansService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<LoomianResponseDto>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomians");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        IEnumerable<LoomianResponseDto>? loomians = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianResponseDto>>();
        return loomians ?? [];
    }

    /// <inheritdoc/>
    public async Task<LoomianResponseDto> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("ID cannot be empty.", nameof(id));
        }
        
        HttpResponseMessage response = httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomians/{id}").Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        LoomianResponseDto? loomian = response.Content.ReadFromJsonAsync<LoomianResponseDto>().Result;
        return loomian ?? throw new KeyNotFoundException($"Loomian with ID {id} not found.");
    
    }
}