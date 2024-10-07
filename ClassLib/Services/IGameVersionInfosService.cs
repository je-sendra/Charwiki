using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for game version info-related operations.
/// </summary>
public interface IGameVersionInfosService
{
    /// <summary>
    /// Get all game version infos.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<GameVersionInfo>> GetAllGameVersionInfosAsync();

    /// <summary>
    /// Get a specific game version info.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<GameVersionInfo> GetByIdAsync(Guid id);

    /// <summary>
    /// Create a new game version info.
    /// </summary>
    /// <param name="gameVersionInfo"></param>
    /// <returns></returns>
    Task<GameVersionInfo> CreateGameVersionInfoAsync(GameVersionInfo gameVersionInfo);
}