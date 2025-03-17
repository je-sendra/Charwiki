namespace Charwiki.WebUi.Services;

/// <summary>
/// A custom HTTP message handler that adds a JWT token to the request headers.
/// </summary>
public class JwtHttpMessageHandler(JwtTokenStoringService jwtTokenStoringService) : DelegatingHandler
{
    /// <summary>
    /// Overrides the SendAsync method to add the JWT token to the request headers.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = jwtTokenStoringService.GetToken();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
