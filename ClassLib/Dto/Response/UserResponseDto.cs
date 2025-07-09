using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a user profile.
/// </summary>
public class UserResponseDto
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The username of the user.
    /// </summary>
    public string Username { get; set; } = string.Empty;
}