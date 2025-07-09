using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for User-related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves the current user's information.
    /// This is typically used to get the user's profile after login.
    /// </summary>
    /// <returns></returns>
    Task<OperationResultWithReturnData<UserResponseDto>> GetMeAsync(string token);

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns></returns>
    Task<OperationResultWithReturnData<UserResponseDto>> GetByIdAsync(Guid id);
}