using Charwiki.ClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for game version info-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class GameVersionInfosController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Endpoint to get all game version infos.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetAllGameVersionInfos()
    {
        return Ok(charwikiDbContext.GameVersionInfos);
    }

    /// <summary>
    /// Endpoint to get a specific game version info.
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

        var gameVersionInfo = charwikiDbContext.GameVersionInfos.FirstOrDefault(e => e.Id == id);
        if (gameVersionInfo == null)
        {
            return NotFound();
        }

        return Ok(gameVersionInfo);
    }

    /// <summary>
    /// Endpoint to create a new game version info.
    /// </summary>
    /// <param name="gameVersionInfo"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles="Admin")]
    public IActionResult CreateGameVersionInfo([FromBody] GameVersionInfo gameVersionInfo)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        charwikiDbContext.GameVersionInfos.Add(gameVersionInfo);
        charwikiDbContext.SaveChanges();

        return Created($"/gameversioninfos/{gameVersionInfo.Id}", gameVersionInfo);
    }
}