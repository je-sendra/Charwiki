using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto.Request;
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
    private const string _controllerName = "gameVersionInfos";

    /// <summary>
    /// Creates a new game version info entry in the database.
    /// This method sends a POST request to the API to create a new game version info.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<OperationResultWithReturnData<GameVersionInfoResponseDto>> CreateAsync(CreateGameVersionInfoRequestDto request)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{apiSettings.Value.BaseUrl}/{_controllerName}", request);
        if (!response.IsSuccessStatusCode)
        {
            return new OperationResultWithReturnData<GameVersionInfoResponseDto>
            {
                HasFailed = true,
                UserMessage = "Failed to create game version info.",
                InternalMessage = $"HTTP Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}"
            };
        }
        GameVersionInfoResponseDto? responseData = await response.Content.ReadFromJsonAsync<GameVersionInfoResponseDto>();
        if (responseData == null)
        {
            return new OperationResultWithReturnData<GameVersionInfoResponseDto>
            {
                HasFailed = true,
                UserMessage = "Failed to retrieve response data. Game version info creation may have failed.",
                InternalMessage = "Response data was null."
            };
        }
        return new OperationResultWithReturnData<GameVersionInfoResponseDto>
        {
            HasFailed = false,
            UserMessage = "Game version info created successfully.",
            InternalMessage = "Operation completed successfully.",
            ReturnData = responseData
        };
    }
}