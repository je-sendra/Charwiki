using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API.Controllers;

/// <summary>
/// The controller for interacting with the Loomian model.
/// </summary>
/// <param name="dataContext">A DataContext for the controller to use.</param>
[Route("[controller]")]
public class MovesController(DataContext dataContext) : Controller
{
    /// <summary>
    /// Gets all the moves.
    /// </summary>
    /// <returns>A list with all the moves.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Move>> Get()
    {
        return dataContext.Moves;
    }

    /// <summary>
    /// Creates a new Move.
    /// </summary>
    /// <param name="move">The move to be created.</param>
    /// <returns>The new move.</returns>
    [HttpPost]
    public ActionResult<Move> Post([FromBody] Move move)
    {
        dataContext.Add(move);
        dataContext.SaveChanges();
        return Created("", move);
    }

    /// <summary>
    /// Gets a move by its id.
    /// </summary>
    /// <param name="id">The id of the move to be retrieved.</param>
    /// <returns>The move with the specified id.</returns>
    [HttpGet("{id}")]
    public ActionResult<Move> GetById(Guid id)
    {
        var move = dataContext.Moves.Find(id);
        if (move == null) return NotFound();
        return move;
    }

    /// <summary>
    /// Updates a move.
    /// </summary>
    /// <param name="id">The id of the move to be updated.</param>
    /// <param name="movePatch">The Move patch.</param>
    /// <returns>The updated Move.</returns>
    [HttpPatch("{id}")]
    public ActionResult<Move> Patch(Guid id, [FromBody] JsonPatchDocument<Move> movePatch)
    {
        var move = dataContext.Moves.Find(id);
        if (move == null) return NotFound();
        movePatch.ApplyTo(move);
        dataContext.SaveChanges();
        return move;
    }
}