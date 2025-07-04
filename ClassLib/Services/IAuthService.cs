using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for authentication-related operations.
/// This service handles user registration and JWT token validation.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user asynchronously.
    /// </summary>
    /// <param name="userRegisterDto"></param>
    /// <returns></returns>
    OperationResult RegisterAsync(UserRegisterDto userRegisterDto);

    /// <summary>
    /// Validates the user login credentials.
    /// If the login is valid, it returns the user; otherwise, it returns an error.
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    OperationResultWithReturnData<UserLoginResponseDto?> LoginAsync(UserLoginDto userLoginDto);
}