using System.Security.Authentication;
using System.Security.Claims;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Models;

namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for authentication-related operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="userRegisterDto"></param>
    /// <returns></returns>
    Task RegisterUserAsync(UserRegisterDto userRegisterDto);

    /// <summary>
    /// Ensures that the specified login is valid.
    /// </summary>
    /// <remarks>
    /// Will throw an exception if the login is invalid, and returns the logged in user if the login is valid.
    /// </remarks>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    /// <exception cref="AuthenticationException"></exception>
    Task<User> EnsureValidLoginAsync(UserLoginDto userLoginDto);

    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string GenerateJwtToken(User user);

    /// <summary>
    /// Gets the user from the specified claims principal.
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    Task<User> GetUserFromClaimsAsync(ClaimsPrincipal claimsPrincipal);
}