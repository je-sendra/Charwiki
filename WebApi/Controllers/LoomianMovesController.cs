using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Controllers.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian move-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianMovesController(CharwikiDbContext charwikiDbContext) : CrudControllerTemplate<LoomianMove>(charwikiDbContext, charwikiDbContext.LoomianMoves)
{
    /// <inheritdoc />
    [HttpPost]
    [Authorize(Roles="Admin")]
    #pragma warning disable S6967
    public override async Task<IActionResult> CreateNewAsync(LoomianMove model)
    {
        return await base.CreateNewAsync(model);
    }
    #pragma warning restore S6967
}