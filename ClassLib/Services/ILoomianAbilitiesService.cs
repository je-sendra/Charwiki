using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

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
    Task<OperationResultWithReturnData<IEnumerable<LoomianAbilityResponseDto>>> GetAllAsync();
}