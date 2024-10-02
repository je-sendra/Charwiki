using Charwiki.ClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian ability-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianAbilitiesController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Endpoint to get all Loomian abilities.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllLoomianAbilities()
    {
        return Ok(charwikiDbContext.LoomianAbilities);
    }

    /// <summary>
    /// Endpoint to get a specific Loomian ability.
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

        var loomianAbility = charwikiDbContext.LoomianAbilities.FirstOrDefault(e => e.Id == id);
        if (loomianAbility == null)
        {
            return NotFound();
        }

        return Ok(loomianAbility);
    }

    /// <summary>
    /// Endpoint to get a Loomian ability by its name. This is a case-sensitive search.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("name/{name}")]
    public IActionResult GetLoomianAbilityIdByName(string name)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomianAbility = charwikiDbContext.LoomianAbilities.FirstOrDefault(e => e.Name == name);
        if (loomianAbility == null)
        {
            return NotFound();
        }

        return Ok(loomianAbility);
    }

    /// <summary>
    /// Endpoint to create a new Loomian ability.
    /// </summary>
    /// <param name="loomianAbility"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles="Admin")]
    public IActionResult CreateLoomianAbility([FromBody] LoomianAbility loomianAbility)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        charwikiDbContext.LoomianAbilities.Add(loomianAbility);
        charwikiDbContext.SaveChanges();

        return Created($"/loomianabilities/{loomianAbility.Id}", loomianAbility);
    }
}