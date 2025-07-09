using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Extensions;
using Charwiki.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian move-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianMovesController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Gets all Loomian moves.
    /// </summary>
    /// <returns>A collection of Loomian moves.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<LoomianMove> loomianMoves = await charwikiDbContext.LoomianMoves.ToListAsync();
        IEnumerable<LoomianMoveResponseDto> responseDtos = loomianMoves
            .Select(loomianMove => loomianMove.ToResponseDto());
        return Ok(responseDtos);
    }

    /// <summary>
    /// Creates a new Loomian move.
    /// </summary>
    /// <param name="requestDto">The request DTO containing the move details.</param>
    /// <returns>The created Loomian move.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateNewAsync([FromBody] CreateLoomianMoveRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await charwikiDbContext.LoomianMoves.AnyAsync(x => x.Name == requestDto.Name))
        {
            return Conflict($"Loomian move with name '{requestDto.Name}' already exists.");
        }

        LoomianMove loomianMove = requestDto.ToEntity();
        await charwikiDbContext.LoomianMoves.AddAsync(loomianMove);
        await charwikiDbContext.SaveChangesAsync();

        LoomianMoveResponseDto responseDto = loomianMove.ToResponseDto();
        return Created($"/loomianMoves/{responseDto.Id}", responseDto);
    }
}