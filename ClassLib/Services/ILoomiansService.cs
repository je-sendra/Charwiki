using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

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
    Task<OperationResultWithReturnData<IEnumerable<LoomianResponseDto>>> GetAllAsync();

    /// <summary>
    /// Gets a Loomian by its unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<OperationResultWithReturnData<LoomianResponseDto>> GetByIdAsync(Guid id);
}