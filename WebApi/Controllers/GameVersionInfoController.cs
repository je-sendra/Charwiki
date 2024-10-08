using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Controllers.Templates;
using Charwiki.WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for game version info-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class GameVersionInfosController(CharwikiDbContext charwikiDbContext) : CrudControllerTemplate<GameVersionInfo>(charwikiDbContext, charwikiDbContext.GameVersionInfos)
{
    /// <inheritdoc />
    [HttpPost]
    [Authorize(Roles = "Admin")]
    #pragma warning disable S6967
    public override async Task<IActionResult> CreateNewAsync([FromBody] GameVersionInfo gameVersionInfo)
    {
        return await base.CreateNewAsync(gameVersionInfo);
    }
    #pragma warning restore S6967
}