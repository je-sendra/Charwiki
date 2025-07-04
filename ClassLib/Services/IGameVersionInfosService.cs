using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Represents a service for game version info-related operations.
/// </summary>
public interface IGameVersionInfosService
{
    /// <summary>
    /// Retrieves all game version information.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<OperationResultWithReturnData<GameVersionInfoResponseDto>> CreateAsync(CreateGameVersionInfoRequestDto request);
}