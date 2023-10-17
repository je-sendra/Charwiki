using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the HeldItem model.
/// </summary>
/// <param name="dataContext">A DataContext for the controller to use.</param>
[Route("[controller]")]
public class HeldItemController(DataContext dataContext) : Controller
{
    /// <summary>
    /// Gets all the HeldItems
    /// </summary>
    /// <returns>A list with all the HeldItems.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<HeldItem>> Get()
    {
        return dataContext.HeldItems;
    }

    /// <summary>
    /// Creates a new HeldItem.
    /// </summary>
    /// <param name="heldItem">The HeldItem to be created.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<HeldItem> Post([FromBody] HeldItem heldItem)
    {
        dataContext.HeldItems.Add(heldItem);
        dataContext.SaveChanges();
        return Created("", heldItem);
    }

    /// <summary>
    /// Get a specific HeldItem by its id.
    /// </summary>
    /// <param name="id">The id to search.</param>
    /// <returns>The HeldItem with the specified Id.</returns>
    [HttpGet("{id}")]
    public ActionResult<HeldItem> GetById(Guid id)
    {
        var heldItem = dataContext.HeldItems.Find(id);
        if (heldItem == null) return NotFound();
        return Ok(heldItem);
    }

    /// <summary>
    /// Updates a HeldItem by its id.
    /// </summary>
    /// <param name="id">The id of the HeldItem to update.</param>
    /// <param name="heldItem">The HeldItem patch.</param>
    /// <returns>The updated HeldItem.</returns>
    [HttpPatch("{id}")]
    public ActionResult<HeldItem> Patch(Guid id, [FromBody] JsonPatchDocument<HeldItem> heldItemPatch)
    {
        var heldItem = dataContext.HeldItems.Find(id);
        if (heldItem == null) return NotFound();
        heldItemPatch.ApplyTo(heldItem);
        dataContext.SaveChanges();
        return heldItem;
    }

}