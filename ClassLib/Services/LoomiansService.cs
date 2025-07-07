using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
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
    public async Task<OperationResultWithReturnData<IEnumerable<LoomianResponseDto>>> GetAllAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomians");
        if (!response.IsSuccessStatusCode)
        {
            return new OperationResultWithReturnData<IEnumerable<LoomianResponseDto>>
            {
                HasFailed = true,
                InternalMessage = response.ReasonPhrase ?? "Failed to retrieve Loomians.",
                UserMessage = "Failed to retrieve Loomians."
            };
        }
        IEnumerable<LoomianResponseDto>? loomians = await response.Content.ReadFromJsonAsync<IEnumerable<LoomianResponseDto>>();
        return new OperationResultWithReturnData<IEnumerable<LoomianResponseDto>>
        {
            HasFailed = false,
            InternalMessage = "Loomians retrieved successfully.",
            UserMessage = "Loomians retrieved successfully.",
            ReturnData = loomians ?? []
        };
    }

    /// <inheritdoc/>
    public async Task<OperationResultWithReturnData<LoomianResponseDto>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResultWithReturnData<LoomianResponseDto>
            {
                HasFailed = true,
                InternalMessage = "Loomian ID cannot be empty.",
                UserMessage = "Invalid Loomian ID provided."
            };
        }

        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/loomians/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return new OperationResultWithReturnData<LoomianResponseDto>
            {
                HasFailed = true,
                InternalMessage = response.ReasonPhrase ?? "Failed to retrieve Loomian.",
                UserMessage = "Failed to retrieve Loomian."
            };
        }
        LoomianResponseDto? loomian = await response.Content.ReadFromJsonAsync<LoomianResponseDto>();
        
        return new OperationResultWithReturnData<LoomianResponseDto>
        {
            HasFailed = false,
            InternalMessage = "Loomian retrieved successfully.",
            UserMessage = "Loomian retrieved successfully.",
            ReturnData = loomian
        };
    }
}