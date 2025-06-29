using System.Security.Authentication;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Services;
using Charwiki.ClassLib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for user-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
/// <param name="authService"></param>
[Route("[controller]")]
public class UserController(CharwikiDbContext charwikiDbContext, IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Endpoint to register a new user.
    /// </summary>
    /// <param name="userRegisterDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await authService.RegisterUserAsync(userRegisterDto);

        return Ok();
    }

    /// <summary>
    /// Endpoint to log in a user.
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Ensure the login is valid
            var loggedInUser = await authService.EnsureValidLoginAsync(userLoginDto);

            // Generate JWT token for the user
            var token = authService.GenerateJwtToken(loggedInUser);

            // Return OK with the token
            return Ok(token);
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(e.Message);
        }
    }

    /// <summary>
    /// Endpoint to get the currently logged in user.
    /// </summary>
    /// <returns></returns>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMeAsync()
    {
        User user = await authService.GetUserFromClaimsAsync(User);

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

        User? user = await charwikiDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.GetCopyWithoutSensitiveInformation());
    }
}