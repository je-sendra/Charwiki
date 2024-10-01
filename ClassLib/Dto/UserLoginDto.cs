namespace Charwiki.ClassLib.Dto;

/// <summary>
/// Represents a user login DTO.
/// </summary>
public class UserLoginDto
{
    /// <summary>
    /// The username of the user.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    public required string Password { get; set; }
}