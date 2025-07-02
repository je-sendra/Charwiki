using Charwiki.ClassLib.Dto.Response;

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
    Task<IEnumerable<TagResponseDto>?> GetAllAsync();

    /// <summary>
    /// Retrieves a tag by its ID.
    /// </summary>
    /// <param name="id">The ID of the tag to retrieve.</param>
    /// <returns>The tag with the specified ID.</returns>
    Task<TagResponseDto?> GetByIdAsync(Guid id);
}