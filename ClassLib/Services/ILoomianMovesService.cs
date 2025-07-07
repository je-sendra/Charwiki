using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Represents a service for Loomian move-related operations.
/// </summary>
public interface ILoomianMovesService
{
    /// <summary>
    /// Gets all Loomian moves.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a collection of Loomian moves.</returns>
    Task<OperationResultWithReturnData<IEnumerable<LoomianMoveResponseDto>>> GetAllAsync();    
}