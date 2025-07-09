namespace Charwiki.ClassLib.Dto.Request;

/// <summary>
/// Represents a user register DTO.
/// </summary>
public class UserRegisterRequestDto
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