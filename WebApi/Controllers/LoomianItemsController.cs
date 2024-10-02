using Charwiki.ClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian item-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianItemsController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Endpoint to get all Loomian items.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllLoomianItems()
    {
        return Ok(charwikiDbContext.LoomianItems);
    }

    /// <summary>
    /// Endpoint to get a specific Loomian item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomianItem = charwikiDbContext.LoomianItems.FirstOrDefault(e => e.Id == id);
        if (loomianItem == null)
        {
            return NotFound();
        }

        return Ok(loomianItem);
    }

    /// <summary>
    /// Endpoint to get a Loomian item by its name. This is a case-sensitive search.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("name/{name}")]
    public IActionResult GetLoomianItemIdByName(string name)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomianItem = charwikiDbContext.LoomianItems.FirstOrDefault(e => e.Name == name);
        if (loomianItem == null)
        {
            return NotFound();
        }

        return Ok(loomianItem);
    }

    /// <summary>
    /// Endpoint to create a new Loomian item.
    /// </summary>
    /// <param name="loomianItem"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles="Admin")]
    public IActionResult CreateLoomianItem([FromBody] LoomianItem loomianItem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        charwikiDbContext.LoomianItems.Add(loomianItem);
        charwikiDbContext.SaveChanges();

        return Created($"/loomianitems/{loomianItem.Id}", loomianItem);
    }
}