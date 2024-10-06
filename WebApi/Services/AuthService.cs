using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Charwiki.ClassLib.Models;
using Microsoft.IdentityModel.Tokens;

namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for authentication-related operations.
/// </summary>
/// <param name="configuration"></param>
/// <param name="charwikiDbContext"></param>
public class AuthService(IConfiguration configuration, CharwikiDbContext charwikiDbContext) : IAuthService
{
    /// <inheritdoc/>
    public string GenerateJwtToken(User user)
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
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
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

    /// <inheritdoc/>
    public User GetUserFromClaims(ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new NoNullAllowedException("User ID not found in claims");

        var user = charwikiDbContext.Users.Find(Guid.Parse(userId));
        if (user == null) throw new NoNullAllowedException("User not found");

        return user;
    }
}