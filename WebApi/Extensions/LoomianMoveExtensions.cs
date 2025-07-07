using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting between LoomianMove entities and DTOs.
/// </summary>
public static class LoomianMoveExtensions
{
    /// <summary>
    /// Converts a LoomianMove entity to a LoomianMoveResponseDto.
    /// </summary>
    /// <param name="loomianMove">The LoomianMove entity to convert.</param>
    /// <returns>A LoomianMoveResponseDto.</returns>
    public static LoomianMoveResponseDto ToResponseDto(this LoomianMove loomianMove)
    {
        return new()
        {
            Id = loomianMove.Id,
            Name = loomianMove.Name,
            Type = loomianMove.Type
        };
    }

    /// <summary>
    /// Converts a CreateLoomianMoveRequestDto to a LoomianMove entity.
    /// </summary>
    /// <param name="requestDto">The request DTO to convert.</param>
    /// <returns>A LoomianMove entity.</returns>
    public static LoomianMove ToEntity(this CreateLoomianMoveRequestDto requestDto)
    {
        return new()
        {
            Name = requestDto.Name,
            Type = requestDto.Type
        };
    }
}