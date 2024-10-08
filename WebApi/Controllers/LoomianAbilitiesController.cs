using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Controllers.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian ability-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class LoomianAbilitiesController(CharwikiDbContext charwikiDbContext) : CrudControllerTemplate<LoomianAbility>(charwikiDbContext, charwikiDbContext.LoomianAbilities)
{
    /// <inheritdoc />
    [HttpPost]
    [Authorize(Roles="Admin")]
    #pragma warning disable S6967
    public override async Task<IActionResult> CreateNewAsync([FromBody] LoomianAbility loomianAbility)
    {
        return await base.CreateNewAsync(loomianAbility);
    }
    #pragma warning restore S6967
}