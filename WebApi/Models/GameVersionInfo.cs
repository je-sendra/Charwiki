using Charwiki.ClassLib.Enums;
using Charwiki.WebApi.Interfaces;

namespace Charwiki.WebApi.Models;

/// <summary>
/// Represents information about a Loomian Legacy game version.
/// </summary>
public class GameVersionInfo : IDatabaseSaveable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The edition of the game.
    /// </summary>
    public required GameEdition GameEdition { get; set; }

    /// <summary>
    /// The version code of the game.
    /// </summary>
    public required string VersionCode { get; set; }

    /// <summary>
    /// The title of the version.
    /// </summary>
    public required string VersionTitle { get; set; }
}