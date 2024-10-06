using System.Security.Claims;
using Charwiki.ClassLib.Models;

namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for authentication-related operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GenerateJwtToken(User user);

    /// <summary>
    /// Gets the user from the specified claims principal.
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    public User GetUserFromClaims(ClaimsPrincipal claimsPrincipal);
}