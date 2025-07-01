using Charwiki.ClassLib.Dto.Request;
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
    /// <param name="includeRatings"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<LoomianSet> GetByIdAsync(Guid id, bool includeValueToStatAssignments = false, bool includeRatings = false);

    /// <summary>
    /// Submits a Loomian set to the server.
    /// These sets will require admin approval before being visible to other users.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <param name="authToken"></param>
    /// <returns></returns>
    Task<LoomianSet> SubmitSetAsync(LoomianSetRequestDto loomianSet, string authToken);

    /// <summary>
    /// Rates a Loomian set with a star rating.
    /// This allows users to express their opinion on the quality of a Loomian set.
    /// A Loomian set can have multiple star ratings from different users.
    /// If a user has already rated a Loomian set, their previous rating will be replaced
    /// with the new rating.
    /// </summary>
    /// <param name="loomianSetId"></param>
    /// <param name="starRating"></param>
    /// <param name="authToken"></param>
    /// <returns></returns>
    Task RateLoomianSetAsync(Guid loomianSetId, int starRating, string authToken);
}