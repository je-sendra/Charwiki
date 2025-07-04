using Charwiki.ClassLib.Enums;

namespace Charwiki.ClassLib.Dto.Request;

/// <summary>
/// Represents a request to create a new game version info.
/// This DTO is used to encapsulate the data required to create a new game version.
/// </summary>
public class CreateGameVersionInfoRequestDto
{
    /// <summary>
    /// Gets or sets the edition of the game.
    /// </summary>
    public GameEdition GameEdition { get; set; }

    /// <summary>
    /// Gets or sets the version code of the game version.
    /// </summary>
    public string? VersionCode { get; set; }

    /// <summary>
    /// Gets or sets the title of the game version.
    /// </summary>
    public string? VersionTitle { get; set; }
}