using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for Loomian stats sets.
/// </summary>
public static class StatsSetExtensions
{
    /// <summary>
    /// Converts a <see cref="CreateStatsSetRequestDto"/> to a <see cref="StatsSet"/>.
    /// </summary>
    /// <param name="statsSetDto">The stats set DTO to convert.</param>
    /// <returns>A new <see cref="StatsSet"/> instance.</returns>
    public static StatsSet ToEntity(this CreateStatsSetRequestDto statsSetDto)
    {
        return new StatsSet
        {
            Health = statsSetDto.Health,
            Energy = statsSetDto.Energy,
            MeleeAttack = statsSetDto.MeleeAttack,
            RangedAttack = statsSetDto.RangedAttack,
            MeleeDefense = statsSetDto.MeleeDefense,
            RangedDefense = statsSetDto.RangedDefense,
            Speed = statsSetDto.Speed
        };
    }

    /// <summary>
    /// Converts a <see cref="StatsSet"/> to a <see cref="StatsSetResponseDto"/>.
    /// </summary>
    /// <param name="statsSet">The stats set to convert.</param>
    /// <returns>A new <see cref="StatsSetResponseDto"/> instance.</returns>
    public static StatsSetResponseDto ToResponseDto(this StatsSet statsSet)
    {
        return new StatsSetResponseDto
        {
            Health = statsSet.Health,
            Energy = statsSet.Energy,
            MeleeAttack = statsSet.MeleeAttack,
            RangedAttack = statsSet.RangedAttack,
            MeleeDefense = statsSet.MeleeDefense,
            RangedDefense = statsSet.RangedDefense,
            Speed = statsSet.Speed
        };
    }
}