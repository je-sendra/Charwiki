namespace Charwiki.WebUi.Services;

/// <summary>
/// Service to handle login state in the application.
/// </summary>
public class LoginStateService
{
    private string? _token = string.Empty;

    /// <summary>
    /// Set the JWT token.
    /// </summary>
    /// <param name="token"></param>
    public void SetToken(string token) => _token = token;

    /// <summary>
    /// Get the JWT token.
    /// </summary>
    /// <returns></returns>
    public string? GetToken() => _token;
}
