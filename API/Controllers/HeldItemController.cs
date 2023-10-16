using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the HeldItem model.
/// </summary>
[Route("[controller]")]
public class HeldItemController : Controller
{
    /// <summary>
    /// Gets all the HeldItems
    /// </summary>
    /// <returns>A list with all the HeldItems.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<HeldItem>> Get()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a new HeldItem.
    /// </summary>
    /// <param name="heldItem">The HeldItem to be created.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<HeldItem> Post([FromBody] HeldItem heldItem)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get a specific HeldItem by its id.
    /// </summary>
    /// <param name="id">The id to search.</param>
    /// <returns>The HeldItem with the specified Id.</returns>
    [HttpGet("{id}")]
    public ActionResult<HeldItem> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates a HeldItem by its id.
    /// </summary>
    /// <param name="id">The id of the HeldItem to update.</param>
    /// <param name="heldItem">The HeldItem with the updated properties.</param>
    /// <returns>The updated HeldItem.</returns>
    [HttpPut("{id}")]
    public ActionResult<HeldItem> Put(Guid id, [FromBody] HeldItem heldItem)
    {
        throw new NotImplementedException();
    }
}