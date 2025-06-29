using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Extensions;
using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    /// Endpoint to submit a Loomian set. These sets are not approved by default and can be reviewed by admins later.
    /// This endpoint can be accessed by any authenticated user.
    /// </summary>
    /// <param name="loomianSetDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Submit([FromBody] LoomianSetDto loomianSetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User user = await authService.GetUserFromClaimsAsync(User);

        LoomianSet loomianSet = loomianSetDto.ToLoomianSet(user.Id);

        loomianSet.EnsureSetIsValid();

        loomianSet.CreationTimestamp = DateTime.UtcNow;

        await charwikiDbContext.LoomianSets.AddAsync(loomianSet);
        await charwikiDbContext.SaveChangesAsync();

        return Created($"/loomianSets/{loomianSet.Id}", loomianSet);
    }


    /// <summary>
    /// Endpoint to approve a Loomian set.
    /// This endpoint can only be accessed by users with the "Admin" role.
    /// </summary>
    /// <param name="loomianSetId"></param>
    /// <returns></returns>
    [HttpPost("{loomianSetId}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approve(Guid loomianSetId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        LoomianSet? loomianSet = await charwikiDbContext.LoomianSets.FirstOrDefaultAsync(ls => ls.Id == loomianSetId);
        if (loomianSet == null)
        {
            return NotFound();
        }

        User user = await authService.GetUserFromClaimsAsync(User);

        loomianSet.ApproverId = user.Id;
        loomianSet.Approved = true;
        loomianSet.ApprovalTimestamp = DateTime.UtcNow;

        charwikiDbContext.LoomianSets.Update(loomianSet);
        await charwikiDbContext.SaveChangesAsync();

        return Ok();
    }
}