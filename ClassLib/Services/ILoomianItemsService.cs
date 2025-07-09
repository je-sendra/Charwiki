using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Represents a service for Loomian item-related operations.
/// </summary>
public interface ILoomianItemsService
{
    /// <summary>
    /// Gets all Loomian items.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing a collection of Loomian items.</returns>
    Task<OperationResultWithReturnData<IEnumerable<LoomianItemResponseDto>>> GetAllAsync();
}