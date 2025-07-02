using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Services;
using Charwiki.ClassLib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for user-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
[Route("[controller]")]
public class UserController(CharwikiDbContext charwikiDbContext) : ControllerBase
{
    /// <summary>
    /// Endpoint to get the currently logged in user.
    /// </summary>
    /// <returns></returns>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMeAsync()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized("User ID not found in claims.");
        Guid userGuid = Guid.Parse(userId);

        User? user = await charwikiDbContext.Users.FindAsync(userGuid);
        if (user == null) return NotFound("User not found.");

        return Ok(user.GetCopyWithoutSensitiveInformation());
    }

    /// <summary>
    /// Endpoint to get a user by their ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? user = await charwikiDbContext.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.GetCopyWithoutSensitiveInformation());
    }
}