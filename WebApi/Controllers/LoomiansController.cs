using Charwiki.WebApi.Extensions;
using Charwiki.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for Loomian-related endpoints.
/// </summary>
[Route("[controller]")]
public class LoomiansController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
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

        Loomian? loomian = await charwikiDbContext.Loomians.FirstOrDefaultAsync(e => e.Name == name);
        if (loomian == null)
        {
            return NotFound();
        }

        return Ok(loomian.ToResponseDto());
    }


    /// <inheritdoc />
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateNewAsync([FromBody] Loomian loomian)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        charwikiDbContext.Loomians.Add(loomian);
        await charwikiDbContext.SaveChangesAsync();
        
        return Created("/loomians/" + loomian.Id, loomian.ToResponseDto());
    }
}