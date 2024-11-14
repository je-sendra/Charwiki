using System.Text.Json;
using Charwiki.ClassLib.Interfaces;

namespace Charwiki.ClassLib.Services.Templates;

#pragma warning disable S3010 // Static fields should not be updated in constructors (csharpsquid:S3010). The mock data is only populated once since there is a check to see if it is already populated in the constructor.

/// <summary>
/// Represents a service with CRUD operations using mock data.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MockCrudControllerServiceTemplate<T> : ICrudControllerServiceTemplate<T> where T : class, IDatabaseSaveable
{

    /// <summary>
    /// Constructor with JSON file.
    /// </summary>
    /// <param name="jsonFileName"></param>
    /// <exception cref="InvalidOperationException"></exception>
    protected MockCrudControllerServiceTemplate(string jsonFileName)
    {
        // If the mock data is already populated, don't do anything.
        if (_mockData.Count > 0) return;

        // Get the path to the JSON file.
        var jsonFilePath = Path.Combine("..", "ClassLib", "MockData", jsonFileName);

        // Deserialize the JSON data.        
        var jsonData = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(jsonFilePath));
        if (jsonData is null)
        {
            throw new InvalidOperationException("Failed to deserialize the JSON data.");
        }

        // Set the mock data.
        _mockData = jsonData;
    }

    /// <summary>
    /// The mock data.
    /// </summary>
    private static List<T> _mockData = new List<T>();

    /// <summary>
    /// Get all items.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(_mockData.AsEnumerable());
    }

    /// <summary>
    /// Get a specific item by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual Task<T> GetByIdAsync(Guid id)
    {
        var foundSet = _mockData.Find(x => x.Id == id);
        if(foundSet == null)
        {
            throw new KeyNotFoundException();
        }
        return Task.FromResult(foundSet);
    }

    /// <summary>
    /// Create a new item.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual Task<T> CreateNewAsync(T model)
    {
        _mockData.Add(model);
        return Task.FromResult(model);
    }
}