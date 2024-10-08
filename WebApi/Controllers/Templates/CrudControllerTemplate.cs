using Charwiki.ClassLib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers.Templates;

/// <summary>
/// Represents a controller with CRUD operations.
/// </summary>
public abstract class CrudControllerTemplate<T>(CharwikiDbContext dbContext, DbSet<T> dbSet) : ControllerBase where T : class, IDatabaseSaveable
{
    /// <summary>
    /// Endpoint to get all items.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        var items = await dbSet.ToListAsync();
        return Ok(items);
    }

    /// <summary>
    /// Endpoint to get a specific item by its id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var item = await dbSet.FindAsync(id);
        return Ok(item);
    }

    /// <summary>
    /// Endpoint to create a new item.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public virtual async Task<IActionResult> CreateNew(T model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        dbSet.Add(model);
        await dbContext.SaveChangesAsync();
        return Ok();
    }
}