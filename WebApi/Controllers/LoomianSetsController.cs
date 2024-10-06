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
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
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

        return Ok(loomianSet);
    }

    /// <summary>
    /// Endpoint to create a new Loomian set.
    /// </summary>
    /// <param name="loomianSetDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles="Admin")]
    public IActionResult CreateLoomianSet([FromBody] LoomianSetDto loomianSetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = authService.GetUserFromClaims(User);
        
        var loomianSet = new LoomianSet
        {
            LoomianId = loomianSetDto.LoomianId,
            PersonalityModifiers = loomianSetDto.PersonalityModifiers,
            LoomianAbilityId = loomianSetDto.AbilityId,
            ItemId = loomianSetDto.ItemId,
            TrainingPoints = loomianSetDto.TrainingPoints,
            UniquePoints = loomianSetDto.UniquePoints,
            Move1Id = loomianSetDto.Move1Id,
            Move2Id = loomianSetDto.Move2Id,
            Move3Id = loomianSetDto.Move3Id,
            Move4Id = loomianSetDto.Move4Id,
            Title = loomianSetDto.Title,
            Explanation = loomianSetDto.Explanation,
            Strategy = loomianSetDto.Strategy,
            Strengths = loomianSetDto.Strengths,
            Weaknesses = loomianSetDto.Weaknesses,
            OtherOptions = loomianSetDto.OtherOptions,
            GameVersionInfoId = loomianSetDto.GameVersionInfoId,
            CreatorId = user.Id
        };

        charwikiDbContext.LoomianSets.Add(loomianSet);
        charwikiDbContext.SaveChanges();

        return Created($"/loomianSets/{loomianSet.Id}", loomianSet);
    }
}