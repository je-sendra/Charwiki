using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Interfaces;

/// <summary>
/// Represents a controller with a get by name operation.
/// </summary>
public interface IGetByNameController
{
    /// <summary>
    /// Endpoint to get an item by its name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<IActionResult> GetByName(string name);
}