using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
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
    public async Task<OperationResultWithReturnData<IEnumerable<TagResponseDto>>> GetAllAsync()
    {
        HttpResponseMessage? response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/tags");
        if (!response.IsSuccessStatusCode)
        {
            return new OperationResultWithReturnData<IEnumerable<TagResponseDto>>
            {
                HasFailed = true,
                UserMessage = "Failed to retrieve tags.",
                InternalMessage = response.ReasonPhrase ?? "Unknown error",
            };
        }
        IEnumerable<TagResponseDto>? tags = await response.Content.ReadFromJsonAsync<IEnumerable<TagResponseDto>>();
        return new OperationResultWithReturnData<IEnumerable<TagResponseDto>>
        {
            HasFailed = false,
            ReturnData = tags ?? [],
            UserMessage = "Successfully retrieved tags.",
            InternalMessage = "Tags retrieved successfully.",
        };
    }

    /// <inheritdoc/>
    public async Task<OperationResultWithReturnData<TagResponseDto>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResultWithReturnData<TagResponseDto>
            {
                HasFailed = true,
                UserMessage = "Invalid tag ID.",
                InternalMessage = "The provided tag ID is empty.",
            };
        }

        HttpResponseMessage? response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/tags/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return new OperationResultWithReturnData<TagResponseDto>
            {
                HasFailed = true,
                UserMessage = "Failed to retrieve tag.",
                InternalMessage = response.ReasonPhrase ?? "Unknown error",
            };
        }
        TagResponseDto? tag = await response.Content.ReadFromJsonAsync<TagResponseDto>();
        return new OperationResultWithReturnData<TagResponseDto>
        {
            HasFailed = false,
            ReturnData = tag,
            UserMessage = "Successfully retrieved tag.",
            InternalMessage = "Tag retrieved successfully.",
        };
    }
}