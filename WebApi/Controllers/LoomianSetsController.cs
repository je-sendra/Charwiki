using System.Security.Claims;
using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Extensions;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Models.OperationResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian set-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianSetsController(CharwikiDbContext charwikiDbContext) : ControllerBase
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
        if (userId == null) return Unauthorized("User ID not found in claims.");
        Guid userGuid = Guid.Parse(userId);

        LoomianSet loomianSet = loomianSetDto.ToLoomianSet(userGuid);
        loomianSet.CreationTimestamp = DateTime.UtcNow;

        OperationResult validationResult = loomianSet.ValidateSet();
        if (validationResult.HasFailed)
        {
            return BadRequest(validationResult.UserMessage);
        }

        await charwikiDbContext.LoomianSets.AddAsync(loomianSet);
        await charwikiDbContext.SaveChangesAsync();

        // If tags are provided, associate them with the Loomian set.
        if (loomianSetDto.TagsIds != null)
        {
            foreach (Guid tagId in loomianSetDto.TagsIds)
            {
                Tag? tag = await charwikiDbContext.Tags.FindAsync(tagId);
                if (tag != null)
                {
                    TagToLoomianSet loomianSetTag = new()
                    {
                        LoomianSetId = loomianSet.Id,
                        TagId = tagId
                    };
                    await charwikiDbContext.TagToLoomianSet.AddAsync(loomianSetTag);
                }
            }
            await charwikiDbContext.SaveChangesAsync();
        }

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
        if (userId == null) return Unauthorized("User ID not found in claims.");

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

        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized("User ID not found in claims.");
        Guid userGuid = Guid.Parse(userId);

        UserToLoomianSetStarRating? existingRating = await charwikiDbContext.UserToLoomianSetStarRatings
            .FirstOrDefaultAsync(r => r.UserId == userGuid && r.LoomianSetId == loomianSetId);

        if (existingRating != null)
        {
            existingRating.StarRating = starRating;
            charwikiDbContext.UserToLoomianSetStarRatings.Update(existingRating);
        }
        else
        {
            UserToLoomianSetStarRating newRating = new()
            {
                UserId = userGuid,
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

        // Include Loomian basic information if the query parameter is set.
        if (queryParams.IncludeLoomian)
        {
            loomianSets = loomianSets
                .Include(ls => ls.Loomian);
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

        if (queryParams.IncludeMetadata)
        {
            loomianSets = loomianSets
                .Include(ls => ls.Creator)
                .Include(ls => ls.Approver)
                .Include(ls => ls.GameVersionInfo);
        }

        if (queryParams.IncludeTags)
        {
            loomianSets = loomianSets
                .Include(ls => ls.Tags)
                .ThenInclude(tls => tls.Tag);
        }

        // Order by average rating if the query parameter is set.
        if (queryParams.IncludeAverageRating)
        {
            loomianSets = loomianSets
                .OrderByDescending(ls => ls.UserToLoomianSetStarRatings!
                    .Average(rating => rating.StarRating));
        }

        if (queryParams.LoomianId.HasValue)
        {
            // Filter by Loomian ID if specified.
            loomianSets = loomianSets
                .Where(ls => ls.LoomianId == queryParams.LoomianId.Value);
        }

        if (queryParams.AbilityId.HasValue)
        {
            // Filter by Ability ID if specified.
            loomianSets = loomianSets
                .Where(ls => ls.LoomianAbilityId == queryParams.AbilityId.Value);
        }

        if (queryParams.ItemId.HasValue)
        {
            // Filter by Item ID if specified.
            loomianSets = loomianSets
                .Where(ls => ls.ItemId == queryParams.ItemId.Value);
        }

        if (queryParams.MoveId.HasValue)
        {
            // Filter by Move ID if specified.
            loomianSets = loomianSets
                .Where(ls => ls.Move1Id == queryParams.MoveId.Value ||
                             ls.Move2Id == queryParams.MoveId.Value ||
                             ls.Move3Id == queryParams.MoveId.Value ||
                             ls.Move4Id == queryParams.MoveId.Value);
        }

        if (queryParams.TagsIds != null && queryParams.TagsIds.Any() && queryParams.IncludeTags)
        {
            // Filter by Tags IDs if specified.
            loomianSets = loomianSets
                .Where(ls => ls.Tags != null)
                .Where(ls => ls.Tags!.Any(t => queryParams.TagsIds.Contains(t.TagId)));
        }

        // Apply pagination if the query parameters specify it.
        int maximumPageSize = 30;
        if (queryParams.PageSize > 0 && queryParams.PageSize < maximumPageSize)
        {
            loomianSets = loomianSets
                .Skip(queryParams.PageNumber * queryParams.PageSize)
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

    /// <summary>
    /// Endpoint to get the rating of a Loomian set by the current user.
    /// </summary>
    /// <param name="loomianSetId"></param>
    /// <returns></returns>
    [HttpGet("{loomianSetId}/myRating")]
    [Authorize]
    public async Task<IActionResult> GetMyRating(Guid loomianSetId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();
        Guid userGuid = Guid.Parse(userId);

        UserToLoomianSetStarRating? rating = await charwikiDbContext.UserToLoomianSetStarRatings
            .FirstOrDefaultAsync(r => r.UserId == userGuid && r.LoomianSetId == loomianSetId);

        if (rating == null)
        {
            return NotFound("You have not rated this Loomian set.");
        }

        return Ok(new StarRatingResponseDto(rating));
    }
}