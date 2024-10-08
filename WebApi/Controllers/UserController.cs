using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Configuration;
using Charwiki.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for user-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
/// <param name="securitySettings"></param>
/// <param name="authService"></param>
[Route("[controller]")]
public class UserController(CharwikiDbContext charwikiDbContext, IOptions<SecuritySettings> securitySettings, IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Endpoint to register a new user.
    /// </summary>
    /// <param name="userRegisterDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Make the username lowercase
        userRegisterDto.Username = userRegisterDto.Username.ToLower();

        // Check if the user already exists
        var existingUser = charwikiDbContext.Users.FirstOrDefault(e => e.Username == userRegisterDto.Username);
        if (existingUser != null)
        {
            return Conflict();
        }

        // Create the user
        var bcryptWorkFactor = securitySettings.Value.BCryptWorkFactor;
        var user = new User
        {
            Username = userRegisterDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password, bcryptWorkFactor),
            Role = UserRole.User
        };

        // Save the user
        charwikiDbContext.Users.Add(user);
        charwikiDbContext.SaveChanges();

        return Ok();
    }

    /// <summary>
    /// Endpoint to log in a user.
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Make the username lowercase
        userLoginDto.Username = userLoginDto.Username.ToLower();

        // Find the user
        var user = charwikiDbContext.Users.FirstOrDefault(e => e.Username == userLoginDto.Username);

        // If the user does not exist, return unauthorized
        if (user == null)
        {
            return Unauthorized();
        }

        // If the password is incorrect, return unauthorized
        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
        {
            return Unauthorized();
        }

        // Generate JWT token for the user
        var token = authService.GenerateJwtToken(user);

        // Return OK
        return Ok(token);
    }
}