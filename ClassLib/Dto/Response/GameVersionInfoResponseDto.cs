using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a game version.
/// </summary>
public class GameVersionInfoResponseDto
{
    /// <summary>
    /// The unique identifier of the game version.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the game version.
    /// </summary>
    public string VersionTitle { get; set; }

    /// <summary>
    /// The code of the game version.
    /// </summary>
    public string VersionCode { get; set; }

    /// <summary>
    /// The edition of the game version.
    /// </summary>
    public GameEdition GameEdition { get; set; }

    /// <summary>
    /// Empty constructor for serialization purposes.
    /// </summary>
    public GameVersionInfoResponseDto()
    {
        Id = Guid.Empty;
        VersionTitle = string.Empty;
        VersionCode = string.Empty;
        GameEdition = GameEdition.Unknown;
    }
}