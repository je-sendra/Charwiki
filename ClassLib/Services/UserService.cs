using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Dto;
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

    /// <inheritdoc/>
    public async Task<User> GetMeAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));
        }

        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/{_controllerRoute}/me");
        response.EnsureSuccessStatusCode();
        User? user = await response.Content.ReadFromJsonAsync<User>();
        return user ?? throw new InvalidOperationException("Failed to retrieve user information.");
    }

    /// <inheritdoc/>
    public async Task<User> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("ID cannot be empty.", nameof(id));
        }

        HttpResponseMessage response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/{_controllerRoute}/{id}");
        response.EnsureSuccessStatusCode();
        User? user = await response.Content.ReadFromJsonAsync<User>();
        return user ?? throw new InvalidOperationException("Failed to retrieve user information.");
    }
}