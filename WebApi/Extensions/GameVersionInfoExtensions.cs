using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting CreateGameVersionInfoRequestDto to GameVersionInfo.
/// </summary>
public static class GameVersionInfoExtensions
{
    /// <summary>
    /// Converts a CreateGameVersionInfoRequestDto to a GameVersionInfo.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public static GameVersionInfo FromCreationDto(this CreateGameVersionInfoRequestDto dto)
    {
        return new GameVersionInfo
        {
            Id = Guid.NewGuid(),
            GameEdition = dto.GameEdition,
            VersionCode = dto.VersionCode ?? string.Empty,
            VersionTitle = dto.VersionTitle ?? string.Empty
        };
    }

    /// <summary>
    /// Converts a GameVersionInfo to a GameVersionInfoResponseDto.
    /// </summary>
    /// <param name="gameVersionInfo"></param>
    /// <returns></returns>
    public static GameVersionInfoResponseDto ToResponseDto(this GameVersionInfo gameVersionInfo)
    {
        return new GameVersionInfoResponseDto
        {
            Id = gameVersionInfo.Id,
            VersionTitle = gameVersionInfo.VersionTitle,
            VersionCode = gameVersionInfo.VersionCode,
            GameEdition = gameVersionInfo.GameEdition
        };
    }
}