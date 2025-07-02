using Charwiki.ClassLib.Dto.Response;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian-related operations.
/// </summary>
public interface ILoomiansService
{
    /// <summary>
    /// Gets all Loomians.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<LoomianResponseDto>> GetAllAsync();

    /// <summary>
    /// Gets a Loomian by its unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<LoomianResponseDto> GetByIdAsync(Guid id);
}