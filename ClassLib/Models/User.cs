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
    /// The password salt of the user.
    /// </summary>
    public string? PasswordSalt { get; set; }

    /// <summary>
    /// The version of password the hash.
    /// </summary>
    public int PasswordHashVersion { get; set; }

    /// <summary>
    /// The role of the user.
    /// </summary>
    public required UserRole Role { get; set; }

    /// <summary>
    /// The set ratings created by the user for Loomian sets.
    /// </summary>
    public virtual List<UserToLoomianSetStarRating>? LoomianSetStarRatings { get; set; }
}