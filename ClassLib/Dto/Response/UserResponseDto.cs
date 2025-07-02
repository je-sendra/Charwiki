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

    /// <summary>
    /// Default constructor with empty properties for serialization purposes.
    /// </summary>
    public UserResponseDto() { }

    /// <summary>
    /// Constructor to create a UserResponseDto from a User model.
    /// </summary>
    /// <param name="user"></param>
    public UserResponseDto(User user)
    {
        Id = user.Id;
        Username = user.Username;
    }
}