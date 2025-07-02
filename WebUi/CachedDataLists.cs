using Charwiki.ClassLib.Dto.Response;

namespace Charwiki.WebUi;

/// <summary>
/// Static data lists for the web UI.
/// WARNING: All data in this class will be reused accross multiple users.
/// This means that any changes made to the lists will be reflected for all users.
/// Use with caution and ensure that the data is appropriate for all users.
/// This class is intended for static data that does not change frequently.
/// </summary>
public static class CachedDataLists
{
    /// <summary>
    /// A list of Loomians available in the game.
    /// </summary>
    public static IEnumerable<LoomianResponseDto> Loomians { get; set; } = [];

    /// <summary>
    /// A list of Loomian abilities available in the game.
    /// </summary>
    public static IEnumerable<LoomianAbilityResponseDto> Abilities { get; set; } = [];

    /// <summary>
    /// A list of Loomian items available in the game.
    /// </summary>
    public static IEnumerable<LoomianItemResponseDto> Items { get; set; } = [];

    /// <summary>
    /// A list of Loomian moves available in the game.
    /// </summary>
    public static IEnumerable<LoomianMoveResponseDto> Moves { get; set; } = [];

    /// <summary>
    /// A list of game version information available in the game.
    /// </summary>
    public static IEnumerable<GameVersionInfoResponseDto> GameVersionInfos { get; set; } = [];

    /// <summary>
    /// A list of tags that can be used to categorize Loomian sets.
    /// </summary>
    public static IEnumerable<TagResponseDto> Tags { get; set; } = [];
}