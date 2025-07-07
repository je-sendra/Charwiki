using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting between LoomianItem entities and DTOs.
/// </summary>
public static class LoomianItemExtensions
{
    /// <summary>
    /// Converts a CreateLoomianItemRequestDto to a LoomianItem entity.
    /// </summary>
    /// <param name="requestDto">The request DTO to convert.</param>
    /// <returns>A LoomianItem entity.</returns>
    public static LoomianItem ToEntity(this CreateLoomianItemRequestDto requestDto)
    {
        return new()
        {
            Name = requestDto.Name
        };
    }

    /// <summary>
    /// Converts a LoomianItem entity to a LoomianItemResponseDto.
    /// </summary>
    /// <param name="loomianItem">The LoomianItem entity to convert.</param>
    /// <returns>A LoomianItemResponseDto.</returns>
    public static LoomianItemResponseDto ToResponseDto(this LoomianItem loomianItem)
    {
        return new LoomianItemResponseDto
        {
            Id = loomianItem.Id,
            Name = loomianItem.Name
        };
    }
}