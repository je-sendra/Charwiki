using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Controllers.Templates;
using Charwiki.WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian-related endpoints.
/// </summary>
[Route("[controller]")]
public class LoomiansController : CrudControllerTemplate<Loomian>, IGetByNameController
{
    private readonly CharwikiDbContext _charwikiDbContext;

    /// <summary>
    /// Constructor for the LoomiansController.
    /// </summary>
    /// <param name="charwikiDbContext"></param>
    public LoomiansController(CharwikiDbContext charwikiDbContext) : base(charwikiDbContext, charwikiDbContext.Loomians)
    {
        _charwikiDbContext = charwikiDbContext;
    }

    /// <summary>
    /// Endpoint to get a Loomian by their name. This is a case-sensitive search.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("name/{name}")]
    public async Task <IActionResult> GetByName(string name)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loomian = await _charwikiDbContext.Loomians.FirstOrDefaultAsync(e => e.Name == name);
        if (loomian == null)
        {
            return NotFound();
        }

        return Ok(loomian);
    }

    
    /// <inheritdoc />
    [HttpPost]
    [Authorize(Roles="Admin")]
    #pragma warning disable S6967
    public override async Task<IActionResult> CreateNewAsync([FromBody] Loomian loomian)
    {
        return await base.CreateNewAsync(loomian);
    }
    #pragma warning restore S6967
}