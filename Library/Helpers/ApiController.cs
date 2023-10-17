using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VewTech.Charwiki.Library.Interfaces;

namespace VewTech.Charwiki.Library.Helpers;

public class ApiController<T> : Controller where T : class, IApiModel
{
    public ApiController(DbContext dbContext, DbSet<T> entities)
    {
        DataContext = dbContext;
        Entities = entities;
    }

    protected DbContext DataContext { get; }
    protected DbSet<T> Entities { get; }

    /// <summary>
    /// Gets all the resources.
    /// </summary>
    /// <returns>A list with all the resources.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<T>> Get()
    {
        return Entities;
    }

    /// <summary>
    /// Creates a new resource.
    /// </summary>
    /// <param name="resource">The resource to be created.</param>
    /// <returns>The newly created resource.</returns>
    [HttpPost]
    public ActionResult<T> Post([FromBody] T resource)
    {
        Entities.Add(resource);
        DataContext.SaveChanges();
        return Created("", resource);
    }

    /// <summary>
    /// Get a specific resource by its id.
    /// </summary>
    /// <param name="id">The id to search.</param>
    /// <returns>The resource with the specified id.</returns>
    [HttpGet("{id}")]
    public ActionResult<T> GetById(Guid id)
    {
        var resource = Entities.Find(id);
        if (resource == null) return NotFound();
        return Ok(resource);
    }

    /// <summary>
    /// Updates a resource by its id.
    /// </summary>
    /// <param name="id">The id of the resource to update.</param>
    /// <param name="heldItemPatch">The resource patch.</param>
    /// <returns>The updated resource.</returns>
    [HttpPatch("{id}")]
    public ActionResult<T> Patch(Guid id, [FromBody] JsonPatchDocument<T> heldItemPatch)
    {
        var resource = Entities.Find(id);
        if (resource == null) return NotFound();
        heldItemPatch.ApplyTo(resource);
        DataContext.SaveChanges();
        return resource;
    }

    /// <summary>
    /// Deletes the resource with the specified id.
    /// </summary>
    /// <param name="id">The id of the resource to delete.</param>
    /// <returns>The action result.</returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var resource = Entities.Find(id);
        if (resource == null) return NotFound();
        Entities.Remove(resource);
        DataContext.SaveChanges();
        return Ok();
    }
}