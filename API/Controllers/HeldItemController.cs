using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    /// The local DataContext object, passed in the constructor.
    /// </summary>
    private DataContext _context { get; set; } = dataContext;

    /// <summary>
    /// Gets all the HeldItems
    /// </summary>
    /// <returns>A list with all the HeldItems.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<HeldItem>> Get()
    {
        return _context.HeldItems;
    }

    /// <summary>
    /// Creates a new HeldItem.
    /// </summary>
    /// <param name="heldItem">The HeldItem to be created.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<HeldItem> Post([FromBody] HeldItem heldItem)
    {
        _context.HeldItems.Add(heldItem);
        _context.SaveChanges();
        return heldItem;
    }

    /// <summary>
    /// Updates a HeldItem by its id. The id cannot be updated.
    /// </summary>
    /// <param name="id">The id of the HeldItem to update.</param>
    /// <param name="heldItem">The HeldItem with the updated properties.</param>
    /// <returns>The updated HeldItem.</returns>
    [HttpPut]
    public ActionResult<HeldItem> Put([FromBody] HeldItem heldItem)
    {
        _context.HeldItems.Update(heldItem);
        _context.SaveChanges();
        return heldItem;
    }

    /// <summary>
    /// Get a specific HeldItem by its id.
    /// </summary>
    /// <param name="id">The id to search.</param>
    /// <returns>The HeldItem with the specified Id.</returns>
    [HttpGet("{id}")]
    public ActionResult<HeldItem?> GetById(Guid id)
    {
        var heldItem = _context.HeldItems.Where(currentHeldItem => currentHeldItem.Id == id).FirstOrDefault();
        return heldItem;
    }

}