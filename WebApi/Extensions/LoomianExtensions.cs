using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting between Loomian entities and DTOs.
/// </summary>
public static class LoomianExtensions
{
    /// <summary>
    /// Converts a Loomian entity to a LoomianResponseDto.
    /// </summary>
    /// <param name="loomian">The Loomian entity to convert.</param>
    /// <returns>A LoomianResponseDto representing the Loomian.</returns>
    public static LoomianResponseDto ToResponseDto(this Loomian loomian)
    {
        return new LoomianResponseDto
        {
            Id = loomian.Id,
            Name = loomian.Name,
            Type1 = loomian.Type1,
            Type2 = loomian.Type2,
            BaseStats = loomian.BaseStats?.ToResponseDto() ?? new StatsSetResponseDto()
        };
    }
}