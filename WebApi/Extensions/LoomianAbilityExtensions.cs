using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting between LoomianAbility entities and DTOs.
/// </summary>
public static class LoomianAbilityExtensions
{
    /// <summary>
    /// Converts a CreateLoomianAbilityRequestDto to a LoomianAbility entity.
    /// </summary>
    /// <param name="requestDto">The request DTO to convert.</param>
    /// <returns>A LoomianAbility entity.</returns>
    public static LoomianAbility ToEntity(this CreateLoomianAbilityRequestDto requestDto)
    {
        return new LoomianAbility
        {
            Name = requestDto.Name
        };
    }

    /// <summary>
    /// Converts a LoomianAbility entity to a LoomianAbilityResponseDto.
    /// </summary>
    /// <param name="loomianAbility">The LoomianAbility entity to convert.</param>
    /// <returns>A LoomianAbilityResponseDto.</returns>
    public static LoomianAbilityResponseDto ToResponseDto(this LoomianAbility loomianAbility)
    {
        return new LoomianAbilityResponseDto
        {
            Id = loomianAbility.Id,
            Name = loomianAbility.Name
        };
    }
}