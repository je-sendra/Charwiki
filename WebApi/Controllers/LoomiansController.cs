using Charwiki.ClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomiansController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Endpoint to get all Loomians.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllLoomians()
    {
        return Ok(charwikiDbContext.Loomians);
    }

    /// <summary>
    /// Endpoint to get a specific Loomian.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomian = charwikiDbContext.Loomians.FirstOrDefault(e => e.Id == id);
        if (loomian == null)
        {
            return NotFound();
        }

        return Ok(loomian);
    }

    /// <summary>
    /// Endpoint to get a Loomian by their name. This is a case-sensitive search.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("name/{name}")]
    public IActionResult GetLoomianIdByName(string name)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomian = charwikiDbContext.Loomians.FirstOrDefault(e => e.Name == name);
        if (loomian == null)
        {
            return NotFound();
        }

        return Ok(loomian);
    }

    /// <summary>
    /// Endpoint to create a new Loomian. The Guid is generated automatically.
    /// </summary>
    /// <param name="loomian"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles="Admin")]
    public IActionResult CreateLoomian([FromBody] Loomian loomian)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Save the Loomian to the database
        charwikiDbContext.Loomians.Add(loomian);
        charwikiDbContext.SaveChanges();

        return Created($"/loomians/{loomian.Id}", loomian);
    }
}