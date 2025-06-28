using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for User-related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="userRegisterDto"></param>
    /// <returns></returns>
    Task RegisterAsync(UserRegisterDto userRegisterDto);

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns>The JWT token for the user.</returns>
    Task<string> LoginAsync(UserLoginDto userLoginDto);

    /// <summary>
    /// Retrieves the current user's information.
    /// This is typically used to get the user's profile after login.
    /// </summary>
    /// <returns></returns>
    Task<User> GetMeAsync(string token);
}