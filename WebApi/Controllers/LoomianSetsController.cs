using System.Security.Claims;
using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
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
    /// <param name="queryParams"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllLoomianSets([FromQuery] LoomianSetQueryParams queryParams)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IEnumerable<LoomianSet> loomianSets = await InternalGetLoomianSetsAsync(queryParams);

        IEnumerable<LoomianSetResponseDto> loomianSetResponse = loomianSets
            .Select(ls => new LoomianSetResponseDto(ls));

        return Ok(loomianSetResponse);
    }

    /// <summary>
    /// Endpoint to get a specific Loomian set.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, [FromQuery] LoomianSetQueryParams queryParams)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        LoomianSet? loomianSet = (await InternalGetLoomianSetsAsync(queryParams, id))
            .FirstOrDefault();

        if (loomianSet == null)
        {
            return NotFound();
        }

        LoomianSetResponseDto loomianSetResponse = new(loomianSet);

        return Ok(loomianSetResponse);
    }

    /// <summary>
    /// Endpoint to submit a Loomian set. These sets are not approved by default and can be reviewed by admins later.
    /// This endpoint can be accessed by any authenticated user.
    /// </summary>
    /// <param name="loomianSetDto"></param>
    /// <returns></returns>
    [HttpPost("submit")]
    [Authorize]
    public async Task<IActionResult> Submit([FromBody] SubmitLoomianSetRequestDto loomianSetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null) return Unauthorized("User ID not found in claims.");
        Guid userGuid = Guid.Parse(userId);

        LoomianSet loomianSet = loomianSetDto.ToLoomianSet(userGuid);

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

        LoomianSet? loomianSet = await charwikiDbContext.LoomianSets.FindAsync(loomianSetId);
        if (loomianSet == null)
        {
            return NotFound();
        }

        if (loomianSet.Approved)
        {
            return BadRequest("This Loomian set is already approved.");
        }

        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(userId == null) return Unauthorized("User ID not found in claims.");

        loomianSet.ApproverId = Guid.Parse(userId);
        loomianSet.Approved = true;
        loomianSet.ApprovalTimestamp = DateTime.UtcNow;

        charwikiDbContext.LoomianSets.Update(loomianSet);
        await charwikiDbContext.SaveChangesAsync();

        return Ok(loomianSet);
    }

    /// <summary>
    /// Endpoint to rate a Loomian set.
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
    public async Task<IActionResult> RateLoomianSet(Guid loomianSetId, [FromBody] int starRating)
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

    /// <summary>
    /// Method to retrieve Loomian sets based on query parameters.
    /// This method is used internally by the controller to fetch Loomian sets.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="setId"></param>
    /// <returns></returns>
    private async Task<IEnumerable<LoomianSet>> InternalGetLoomianSetsAsync(LoomianSetQueryParams queryParams, Guid setId = default)
    {
        IQueryable<LoomianSet> loomianSets = charwikiDbContext.LoomianSets;
        
        // If a specific Loomian set ID is provided, only retrieve that set.
        // This allows for more efficient queries when looking for a single set.
        if (setId != Guid.Empty)
        {
            loomianSets = loomianSets.Where(ls => ls.Id == setId);
        }

        // Only include Loomian sets that are approved if the query parameter is set.
        if (queryParams.HideNonApprovedSets)
        {
            loomianSets = loomianSets.Where(ls => ls.Approved);
        }

        // Only include related ValueToStatAssignments if the query parameter is set.
        if (queryParams.IncludeValueToStatAssignments)
        {
            loomianSets = loomianSets
                .Include(ls => ls.PersonalityModifiers)
                .Include(ls => ls.UniquePoints)
                .Include(ls => ls.TrainingPoints);
        }

        // Include ratings if the query parameter is set.
        if (queryParams.IncludeAverageRating)
        {
            loomianSets = loomianSets
                .Include(ls => ls.UserToLoomianSetStarRatings);
        }

        // Include ability if the query parameter is set.
        if (queryParams.IncludeAbility)
        {
            loomianSets = loomianSets
                .Include(ls => ls.Ability);
        }

        // Include item if the query parameter is set.
        if (queryParams.IncludeItem)
        {
            loomianSets = loomianSets
                .Include(ls => ls.Item);
        }

        // Include moves if the query parameter is set.
        if (queryParams.IncludeMoves)
        {
            loomianSets = loomianSets
                .Include(ls => ls.Move1)
                .Include(ls => ls.Move2)
                .Include(ls => ls.Move3)
                .Include(ls => ls.Move4);
        }

        // Apply pagination if the query parameters specify it.
        int maximumPageSize = 30;
        if (queryParams.PageSize > 0 && queryParams.PageSize < maximumPageSize)
        {
            loomianSets = loomianSets
                .Skip(queryParams.Page * queryParams.PageSize)
                .Take(queryParams.PageSize);
        }
        else
        {
            // Limit the number of results to prevent excessive data retrieval.
            loomianSets = loomianSets.Take(maximumPageSize);
        }

        // The database query is executed here.
        // This is where the actual data retrieval happens.
        IEnumerable<LoomianSet> result = await loomianSets
            .ToListAsync();

        // Do not return long explanations if the query parameter is not set.
        if (!queryParams.IncludeDetailedExplanation)
        {
            foreach (LoomianSet loomianSet in result)
            {
                loomianSet.Strategy = null;
                loomianSet.Strengths = null;
                loomianSet.Weaknesses = null;
                loomianSet.OtherOptions = null;
            }
        }

        return result;
    }
}