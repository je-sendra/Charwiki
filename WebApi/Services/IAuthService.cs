using System.Security.Authentication;
using System.Security.Claims;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Models.OperationResult;

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
    Task<OperationResult> RegisterUserAsync(UserRegisterDto userRegisterDto);

    /// <summary>
    /// Ensures that the specified login is valid.
    /// </summary>
    /// <remarks>
    /// Will return the user if the login is valid, or an error if it is not.
    /// </remarks>
    /// <param name="userLoginDto"></param>
    Task<OperationResultWithReturnData<User?>> ValidateLogin(UserLoginDto userLoginDto);

    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    string GenerateJwtToken(User user);
}