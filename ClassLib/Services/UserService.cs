using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto;
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
    public async Task<string> LoginAsync(UserLoginDto userLoginDto)
    {
        var response = await httpClient.PostAsJsonAsync($"{apiSettings.Value.BaseUrl}/{_controllerRoute}/login", userLoginDto);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadAsStringAsync();
        return token;
    }

    /// <inheritdoc/>
    public async Task RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var response = await httpClient.PostAsJsonAsync($"{apiSettings.Value.BaseUrl}/{_controllerRoute}/register", userRegisterDto);
        response.EnsureSuccessStatusCode();
    }
}