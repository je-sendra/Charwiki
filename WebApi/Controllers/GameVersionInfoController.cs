using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Extensions;
using Charwiki.WebApi.Models;
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
    /// Endpoint to create a new game version info.
    /// This endpoint allows admins to create a new game version info entry in the database.
    /// </summary>
    /// <param name="createGameVersionInfoRequestDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateNewAsync([FromBody] CreateGameVersionInfoRequestDto createGameVersionInfoRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Convert the request DTO to a GameVersionInfo entity
        GameVersionInfo gameVersionInfo = createGameVersionInfoRequestDto.FromCreationDto();
        charwikiDbContext.GameVersionInfos.Add(gameVersionInfo);
        await charwikiDbContext.SaveChangesAsync();

        // Convert the GameVersionInfo entity to a response DTO
        GameVersionInfoResponseDto responseDto = gameVersionInfo.ToResponseDto();

        return Created($"/gameVersionInfos/{responseDto.Id}", responseDto);
    }
}