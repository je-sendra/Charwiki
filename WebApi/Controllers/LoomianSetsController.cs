using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian set-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
/// <param name="authService"></param>
[Route("[controller]")]
public class LoomianSetsController(CharwikiDbContext charwikiDbContext, IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Endpoint to get all Loomian sets.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllLoomianSets()
    {
        return Ok(charwikiDbContext.LoomianSets);
    }

    /// <summary>
    /// Endpoint to get a specific Loomian set.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeValueToStatAssignments"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id, bool includeValueToStatAssignments = false)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomianSet = charwikiDbContext.LoomianSets.FirstOrDefault(e => e.Id == id);
        if (loomianSet == null)
        {
            return NotFound();
        }

        // If the user wants to include the value to stat assignments, include them in the response.
        if (includeValueToStatAssignments)
        {
            // Load the personality modifiers for the Loomian set.
            charwikiDbContext.Entry(loomianSet)
                .Collection(ls => ls.PersonalityModifiers)
                .Load();

            // Load the unique points for the Loomian set.
            charwikiDbContext.Entry(loomianSet)
                .Collection(ls => ls.UniquePoints)
                .Load();

            // Load the training points for the Loomian set.
            charwikiDbContext.Entry(loomianSet)
                .Collection(ls => ls.TrainingPoints)
                .Load();
        }

        return Ok(loomianSet);
    }

    /// <summary>
    /// Endpoint to create a new Loomian set.
    /// </summary>
    /// <param name="loomianSetDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateLoomianSet([FromBody] LoomianSetDto loomianSetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await authService.GetUserFromClaimsAsync(User);

        var loomianSet = loomianSetDto.ToLoomianSet(user.Id);

        charwikiDbContext.LoomianSets.Add(loomianSet);
        await charwikiDbContext.SaveChangesAsync();

        return Created($"/loomianSets/{loomianSet.Id}", loomianSet);
    }
}