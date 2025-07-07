using Charwiki.ClassLib.Enums;

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
    public string VersionTitle { get; set; } = string.Empty;

    /// <summary>
    /// The code of the game version.
    /// </summary>
    public string VersionCode { get; set; } = string.Empty;

    /// <summary>
    /// The edition of the game version.
    /// </summary>
    public GameEdition GameEdition { get; set; }
}