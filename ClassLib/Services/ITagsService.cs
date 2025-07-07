using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service interface for managing tags
/// </summary>
public interface ITagsService
{
    /// <summary>
    /// Retrieves all tags.
    /// </summary>
    /// <returns>A collection of tags.</returns>
    Task<OperationResultWithReturnData<IEnumerable<TagResponseDto>>> GetAllAsync();

    /// <summary>
    /// Retrieves a tag by its ID.
    /// </summary>
    /// <param name="id">The ID of the tag to retrieve.</param>
    /// <returns>The tag with the specified ID.</returns>
    Task<OperationResultWithReturnData<TagResponseDto>> GetByIdAsync(Guid id);
}