using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian-related operations.
/// </summary>
public interface ILoomiansService
{
    /// <summary>
    /// Gets the loomians.
    /// </summary>
    /// <returns>The loomians.</returns>
    Task<IEnumerable<Loomian>> GetAllLoomiansAsync();

    /// <summary>
    /// Gets a loomian by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Loomian> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets a loomian by its name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<Loomian> GetLoomianByNameAsync(string name);

    /// <summary>
    /// Creates a loomian.
    /// </summary>
    /// <param name="loomian"></param>
    /// <returns></returns>
    Task<Loomian> CreateLoomianAsync(Loomian loomian);
}