using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for authentication-related operations.
/// </summary>
/// <param name="securitySettings"></param>
/// <param name="charwikiDbContext"></param>
public class AuthService(IOptions<SecuritySettings> securitySettings, CharwikiDbContext charwikiDbContext) : IAuthService
{
    /// <inheritdoc/>
    public string GenerateJwtToken(User user)
    {

        var secret = securitySettings.Value.JwtSettings.Secret;
        var issuer = securitySettings.Value.JwtSettings.Issuer;
        var audience = securitySettings.Value.JwtSettings.Audience;
        var expiryMinutes = securitySettings.Value.JwtSettings.ExpiryMinutes;

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

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