using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Interfaces;

namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents a user of the Charwiki application.
/// </summary>
public class User : IDatabaseSaveable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The username of the user.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The password hash of the user.
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// The role of the user.
    /// </summary>
    public required UserRole Role { get; set; }
}