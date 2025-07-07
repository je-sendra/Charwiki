using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for User-related operations against the API.
/// </summary>
/// <param name="httpClient"></param>
/// <param name="apiSettings"></param>
public class UserService(HttpClient httpClient, IOptions<ApiSettings> apiSettings) : IUserService
{
    private readonly string _controllerRoute = "user";

    /// <inheritdoc/>
    public async Task<OperationResultWithReturnData<UserResponseDto>> GetMeAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return new()
            {
                HasFailed = true,
                InternalMessage = "Token cannot be null or empty.",
                UserMessage = "You must be logged in to perform this action."
            };
        }

        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/{_controllerRoute}/me");
        response.EnsureSuccessStatusCode();
        UserResponseDto? user = await response.Content.ReadFromJsonAsync<UserResponseDto>();
        return new OperationResultWithReturnData<UserResponseDto>
        {
            HasFailed = false,
            InternalMessage = "User retrieved successfully.",
            UserMessage = "User retrieved successfully.",
            ReturnData = user
        };
    }

    /// <inheritdoc/>
    public async Task<OperationResultWithReturnData<UserResponseDto>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResultWithReturnData<UserResponseDto>
            {
                HasFailed = true,
                InternalMessage = "User ID cannot be empty.",
                UserMessage = "Invalid user ID provided."
            };
        }

        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/{_controllerRoute}/{id}");
        response.EnsureSuccessStatusCode();
        UserResponseDto? user = await response.Content.ReadFromJsonAsync<UserResponseDto>();
        return new OperationResultWithReturnData<UserResponseDto>
        {
            HasFailed = false,
            InternalMessage = "User retrieved successfully.",
            UserMessage = "User retrieved successfully.",
            ReturnData = user
        };
    }
}