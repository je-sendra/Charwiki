using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for managing tags against the Web API.
/// </summary>
/// <param name="apiSettings"></param>
/// <param name="httpClient"></param>
public class TagsService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : ITagsService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<TagResponseDto>?> GetAllAsync()
    {
        HttpResponseMessage? response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/tags");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        IEnumerable<TagResponseDto>? tags = await response.Content.ReadFromJsonAsync<IEnumerable<TagResponseDto>>();
        return tags;
    }

    /// <inheritdoc/>
    public async Task<TagResponseDto?> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("ID cannot be empty.", nameof(id));
        }

        HttpResponseMessage? response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/tags/{id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync() ?? response.ReasonPhrase);
        }
        TagResponseDto? tag = await response.Content.ReadFromJsonAsync<TagResponseDto>();
        return tag;
    }
}