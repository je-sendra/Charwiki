using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Extensions;
using Charwiki.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian ability-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LomianAbilitiesController(CharwikiDbContext charwikiDbContext) : ControllerBase
{ 

    /// <summary>
    /// Gets all Loomian abilities.
    /// </summary>
    /// <returns>A collection of Loomian abilities.</returns>
    public async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<LoomianAbility> loomianAbilities = await charwikiDbContext.LoomianAbilities.ToListAsync();
        IEnumerable<LoomianAbilityResponseDto> responseDtos = loomianAbilities.Select(loomianAbility => loomianAbility.ToResponseDto());
        return Ok(responseDtos);
    }

    /// <inheritdoc />
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateLoomianAbilityRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Convert the request DTO to a LoomianAbility entity
        LoomianAbility loomianAbility = requestDto.ToEntity();

        charwikiDbContext.LoomianAbilities.Add(loomianAbility);
        await charwikiDbContext.SaveChangesAsync();

        LoomianAbilityResponseDto responseDto = loomianAbility.ToResponseDto();
        return Created("/loomianAbilities", responseDto);
    }
}