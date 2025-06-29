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
    /// <param name="includeRatings"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, bool includeValueToStatAssignments = false, bool includeRatings = false)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        LoomianSet? loomianSet = await charwikiDbContext.LoomianSets.FirstOrDefaultAsync(e => e.Id == id);
        if (loomianSet == null)
        {
            return NotFound();
        }

        // If the user wants to include the value to stat assignments, include them in the response.
        if (includeValueToStatAssignments)
        {
            // Load the personality modifiers for the Loomian set.
            await charwikiDbContext.Entry(loomianSet)
                .Collection(ls => ls.PersonalityModifiers)
                .LoadAsync();

            // Load the unique points for the Loomian set.
            await charwikiDbContext.Entry(loomianSet)
                .Collection(ls => ls.UniquePoints)
                .LoadAsync();

            // Load the training points for the Loomian set.
            await charwikiDbContext.Entry(loomianSet)
                .Collection(ls => ls.TrainingPoints)
                .LoadAsync();
        }

        // If the user wants to include the ratings, include them in the response.
        if (includeRatings)
        {
            // Load the star ratings for the Loomian set.
            loomianSet.UserToLoomianSetStarRatings = await charwikiDbContext.UserToLoomianSetStarRatings
                .Where(x => x.LoomianSetId == id)
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .ToListAsync();
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

    /// <summary>
    /// Endpoint to star a Loomian set.
    /// This endpoint can be accessed by any authenticated user.
    /// Users can rate a Loomian set with a star rating from 1 to 5.
    /// If the user has already rated the Loomian set, their rating will be updated.
    /// If the user has not rated the Loomian set, a new rating will be created
    /// </summary>
    /// <param name="loomianSetId"></param>
    /// <param name="starRating"></param>
    /// <returns></returns>
    [HttpPost("{loomianSetId}/rate")]
    [Authorize]
    public async Task<IActionResult> StarLoomianSet(Guid loomianSetId, [FromBody] int starRating)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (starRating < 1 || starRating > 5)
        {
            return BadRequest("Star rating must be between 1 and 5.");
        }

        LoomianSet? loomianSet = await charwikiDbContext.LoomianSets.FirstOrDefaultAsync(ls => ls.Id == loomianSetId);
        if (loomianSet == null)
        {
            return NotFound();
        }

        User user = await authService.GetUserFromClaimsAsync(User);

        UserToLoomianSetStarRating? existingRating = await charwikiDbContext.UserToLoomianSetStarRatings
            .FirstOrDefaultAsync(r => r.UserId == user.Id && r.LoomianSetId == loomianSetId);

        if (existingRating != null)
        {
            existingRating.StarRating = starRating;
            charwikiDbContext.UserToLoomianSetStarRatings.Update(existingRating);
        }
        else
        {
            UserToLoomianSetStarRating newRating = new()
            {
                UserId = user.Id,
                LoomianSetId = loomianSetId,
                StarRating = starRating
            };
            await charwikiDbContext.UserToLoomianSetStarRatings.AddAsync(newRating);
        }

        await charwikiDbContext.SaveChangesAsync();

        return Ok();
    }
}