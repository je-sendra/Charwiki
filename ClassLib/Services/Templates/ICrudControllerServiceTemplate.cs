using Charwiki.ClassLib.Interfaces;

namespace Charwiki.ClassLib.Services.Templates;

/// <summary>
/// Represents a service with CRUD operations.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrudControllerServiceTemplate<T> where T : class, IDatabaseSaveable
{
    /// <summary>
    /// Get all items.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Get a specific item by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> GetByIdAsync(Guid id);

    /// <summary>
    /// Create a new item.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<T> CreateNewAsync(T model);
}