using Charwiki.ClassLib.Enums;

namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Represents the response data for a user login operation.
/// </summary>
public class UserLoginResponseDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the roles assigned to the user.
    /// </summary>
    public UserRoles Roles { get; set; }

    /// <summary>
    /// Gets or sets the authentication token for the user.
    /// </summary>
    public string Token { get; set; } = null!;
}