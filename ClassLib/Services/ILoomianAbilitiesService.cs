using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian ability-related operations.
/// </summary>
public interface ILoomianAbilitiesService
{
    /// <summary>
    /// Get all Loomian abilities.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<LoomianAbility>> GetAllAbilitiesAsync();

    /// <summary>
    /// Get a specific Loomian ability.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<LoomianAbility> GetByIdAsync(int id);

    /// <summary>
    /// Get a Loomian ability by its name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<LoomianAbility> GetByNameAsync(string name);

    /// <summary>
    /// Create a new Loomian ability.
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    Task<LoomianAbility> CreateAsync(LoomianAbility ability);
}