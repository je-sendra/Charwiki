using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Charwiki.WebApi.Controllers;

/// <summary>
/// Controller for user-related endpoints.
/// </summary>
/// <param name="charwikiDbContext"></param>
/// <param name="configuration"></param>
[Route("api/[controller]")]
public class UserController(CharwikiDbContext charwikiDbContext, IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Endpoint to register a new user.
    /// </summary>
    /// <param name="userRegisterDto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserLoginDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the user already exists
        var existingUser = charwikiDbContext.Users.FirstOrDefault(e => e.Username == userRegisterDto.Username);
        if (existingUser != null)
        {
            return Conflict();
        }

        // Create the user
        var bcryptWorkFactor = configuration.GetValue<int>("BcryptWorkFactor");
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
        var token = GenerateJwtToken(user);

        // Return OK
        return Ok(token);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? throw new NoNullAllowedException("Secret not found in configuration");
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expiryMinutesString = jwtSettings["ExpiryMinutes"] ?? throw new NoNullAllowedException("ExpiryMinutes not found in configuration");
        var expiryMinutes = int.Parse(expiryMinutesString);

        // Disable warning because the secret key is not hardcoded
        #pragma warning disable S6781
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        #pragma warning restore S6781

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}