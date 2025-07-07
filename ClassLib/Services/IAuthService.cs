using Charwiki.ClassLib.Dto.Request;
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
    Task<OperationResult> RegisterAsync(UserRegisterRequestDto userRegisterDto);

    /// <summary>
    /// Validates the user login credentials.
    /// If the login is valid, it returns the user; otherwise, it returns an error.
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    Task<OperationResultWithReturnData<UserLoginResponseDto>> LoginAsync(UserLoginRequestDto userLoginDto);
}