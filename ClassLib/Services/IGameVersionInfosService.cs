using Charwiki.ClassLib.Dto.Response;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Represents a service for game version info-related operations.
/// </summary>
public interface IGameVersionInfosService
{
    /// <summary>
    /// Gets all game version infos.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a collection of game version infos.</returns>
    Task<IEnumerable<GameVersionInfoResponseDto>> GetAllAsync();
}