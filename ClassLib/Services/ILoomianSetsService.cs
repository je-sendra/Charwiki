using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Services.Templates;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Represents a service for Loomian set-related operations.
/// </summary>
public interface ILoomianSetsService : ICrudControllerServiceTemplate<LoomianSet>
{
    /// <summary>
    /// Get a specific Loomian set by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeValueToStatAssignments"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<LoomianSet> GetByIdAsync(Guid id, bool includeValueToStatAssignments = false);
}