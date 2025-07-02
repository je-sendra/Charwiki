using Charwiki.ClassLib.Dto.Response;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Represents a service for Loomian ability-related operations.
/// </summary>
public interface ILoomianAbilitiesService
{
    /// <summary>
    /// Gets all Loomian abilities.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a collection of Loomian abilities.</returns>
    Task<IEnumerable<LoomianAbilityResponseDto>> GetAllAsync();
}