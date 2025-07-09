using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting between User entities and DTOs.
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Converts a User entity to a UserResponseDto.
    /// </summary>
    /// <param name="user">The User entity to convert.</param>
    /// <returns>A UserResponseDto representing the user.</returns>
    public static UserResponseDto ToResponseDto(this User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}