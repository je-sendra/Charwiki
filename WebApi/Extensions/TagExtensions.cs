using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting between Tag entities and DTOs.
/// </summary>
public static class TagExtensions
{
    /// <summary>
    /// Converts a Tag entity to a TagResponseDto.
    /// </summary>
    /// <param name="tag">The Tag entity to convert.</param>
    /// <returns>A TagResponseDto.</returns>
    public static TagResponseDto ToResponseDto(this Tag tag)
    {
        return new()
        {
            Id = tag.Id,
            Name = tag.Name,
            Description = tag.Description
        };
    }

    /// <summary>
    /// Converts a CreateTagRequestDto to a Tag entity.
    /// </summary>
    /// <param name="requestDto">The request DTO to convert.</param>
    /// <returns>A Tag entity.</returns>
    public static Tag ToEntity(this CreateTagRequestDto requestDto)
    {
        return new()
        {
            Name = requestDto.Name,
            Description = requestDto.Description
        };
    }
}