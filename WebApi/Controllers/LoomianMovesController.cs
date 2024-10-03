using Charwiki.ClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian move-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianMovesController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Endpoint to get all Loomian moves.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllLoomianMoves()
    {
        return Ok(charwikiDbContext.LoomianMoves);
    }

    /// <summary>
    /// Endpoint to get a specific Loomian move.
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

        var loomianMove = charwikiDbContext.LoomianMoves.FirstOrDefault(e => e.Id == id);
        if (loomianMove == null)
        {
            return NotFound();
        }

        return Ok(loomianMove);
    }

    /// <summary>
    /// Endpoint to get a Loomian move by its name. This is a case-sensitive search.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("name/{name}")]
    public IActionResult GetLoomianMoveIdByName(string name)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomianMove = charwikiDbContext.LoomianMoves.FirstOrDefault(e => e.Name == name);
        if (loomianMove == null)
        {
            return NotFound();
        }

        return Ok(loomianMove);
    }

    /// <summary>
    /// Endpoint to create a new Loomian move.
    /// </summary>
    /// <param name="loomianMove"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles="Admin")]
    public IActionResult CreateLoomianMove([FromBody] LoomianMove loomianMove)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        charwikiDbContext.LoomianMoves.Add(loomianMove);
        charwikiDbContext.SaveChanges();

        return Created($"/loomianmoves/{loomianMove.Id}", loomianMove);
    }
}