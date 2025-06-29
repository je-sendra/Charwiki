using Charwiki.ClassLib.Dto;
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

    /// <summary>
    /// Submits a Loomian set to the server.
    /// These sets will require admin approval before being visible to other users.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <param name="authToken"></param>
    /// <returns></returns>
    Task<LoomianSet> SubmitSetAsync(LoomianSetDto loomianSet, string authToken);
}