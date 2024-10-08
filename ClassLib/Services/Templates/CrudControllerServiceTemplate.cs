using System.Net.Http.Json;
using Charwiki.ClassLib.Configuration;
using Charwiki.ClassLib.Interfaces;
using Microsoft.Extensions.Options;

namespace Charwiki.ClassLib.Services.Templates;

/// <summary>
/// Represents a service with CRUD operations.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class CrudControllerServiceTemplate<T>(HttpClient httpClient, IOptions<ApiSettings> apiSettings, string controllerRoute) : ICrudControllerServiceTemplate<T> where T : class, IDatabaseSaveable
{
    /// <summary>
    /// Get all items.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/{controllerRoute}");
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
        if (items is null)
        {
            throw new InvalidOperationException("Failed to deserialize the items.");
        }
        return items;
    }

    /// <summary>
    /// Get a specific item by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"{apiSettings.Value.BaseUrl}/{controllerRoute}/{id}");
        response.EnsureSuccessStatusCode();
        var item = await response.Content.ReadFromJsonAsync<T>();
        if (item is null)
        {
            throw new InvalidOperationException("Failed to deserialize the item.");
        }
        return item;
    }

    /// <summary>
    /// Create a new item.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task<T> CreateNewAsync(T model)
    {
        var response = await httpClient.PostAsJsonAsync($"{apiSettings.Value.BaseUrl}/gameversioninfos", model);
        response.EnsureSuccessStatusCode();
        var createdItem = await response.Content.ReadFromJsonAsync<T>();
        if (createdItem is null)
        {
            throw new InvalidOperationException("Failed to deserialize the created item.");
        }
        return createdItem;
    }
}