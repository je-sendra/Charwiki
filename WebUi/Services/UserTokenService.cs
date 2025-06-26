using Blazored.LocalStorage;

namespace Charwiki.WebUi.Services;

/// <summary>
/// Service for managing user authentication tokens in local storage.
/// </summary>
/// <param name="localStorageService"></param>
public class UserTokenService(ILocalStorageService localStorageService)
{
    private readonly string _authTokenLocalStorageName = "authToken";

    /// <summary>
    /// Retrieves the authentication token from local storage.
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetAuthTokenAsync()
    {
        return await localStorageService.GetItemAsync<string>(_authTokenLocalStorageName);
    }

    /// <summary>
    /// Sets the authentication token in local storage.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task SetAuthTokenAsync(string token)
    {
        await localStorageService.SetItemAsync(_authTokenLocalStorageName, token);
    }

    /// <summary>
    /// Removes the authentication token from local storage.
    /// </summary>
    /// <returns></returns>
    public async Task RemoveAuthTokenAsync()
    {
        await localStorageService.RemoveItemAsync(_authTokenLocalStorageName);
    }

    /// <summary>
    /// Checks if the user is authenticated by verifying the presence of an authentication token in local storage.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> IsAuthenticatedAsync()
    {
        string? token = await this.GetAuthTokenAsync();
        return !string.IsNullOrEmpty(token);
    }
}