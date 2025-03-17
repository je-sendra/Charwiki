namespace Charwiki.WebUi.Services;

/// <summary>
/// Service to store and retrieve JWT token.
/// </summary>
public class JwtTokenStoringService
{
    private string? _token = string.Empty;

    /// <summary>
    /// Set the token.
    /// </summary>
    /// <param name="token"></param>
    public void SetToken(string token) => _token = token;

    /// <summary>
    /// Get the token.
    /// </summary>
    /// <returns></returns>
    public string? GetToken() => _token;
}
