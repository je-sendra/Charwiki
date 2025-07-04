using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Charwiki.ClassLib.Dto;
using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;
using Charwiki.WebApi.Configuration;
using Charwiki.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for authentication-related operations.
/// </summary>
/// <param name="securitySettings"></param>
/// <param name="passwordHashVersionHistoryService"></param>
/// <param name="passwordHashingService"></param>
/// <param name="charwikiDbContext"></param>
public class AuthService(IOptions<SecuritySettings> securitySettings, IPasswordHashVersionHistoryService passwordHashVersionHistoryService, IPasswordHashingService passwordHashingService, CharwikiDbContext charwikiDbContext) : IAuthService
{
    /// <inheritdoc/>
    public async Task<OperationResult> RegisterUserAsync(UserRegisterDto userRegisterDto)
    {
        // Make the username lowercase
        userRegisterDto.Username = userRegisterDto.Username.ToLower();

        // Check if the user already exists
        User? existingUser = await charwikiDbContext.Users.FirstOrDefaultAsync(e => e.Username == userRegisterDto.Username);
        if (existingUser != null)
        {
            string errorMessage = $"User with username '{userRegisterDto.Username}' already exists.";
            return new OperationResult()
            {
                HasFailed = true,
                InternalMessage = errorMessage,
                UserMessage = errorMessage
            };
        }

        // Hash the password
        var (hashedPassword, salt) = passwordHashingService.HashPassword(userRegisterDto.Password);

        // Create the user
        var user = new User
        {
            Username = userRegisterDto.Username,
            PasswordHash = hashedPassword,
            PasswordSalt = salt,
            PasswordHashVersion = passwordHashVersionHistoryService.GetLatestVersion(),
            Roles = UserRoles.None
        };

        // Save the user
        charwikiDbContext.Users.Add(user);
        await charwikiDbContext.SaveChangesAsync();

        string successMessage = $"User '{user.Username}' registered successfully.";
        return new OperationResult()
        {
            HasFailed = false,
            InternalMessage = successMessage,
            UserMessage = successMessage
        };
    }

    /// <inheritdoc/>
    public string GenerateJwtToken(User user)
    {
        var secret = securitySettings.Value.JwtSettings.Secret;
        var issuer = securitySettings.Value.JwtSettings.Issuer;
        var audience = securitySettings.Value.JwtSettings.Audience;
        var expiryMinutes = securitySettings.Value.JwtSettings.ExpiryMinutes;

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
        {
            if (role != UserRoles.None && user.Roles.HasFlag(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
        }

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
    public async Task<OperationResultWithReturnData<User?>> ValidateLogin(UserLoginDto userLoginDto)
    {
        // Make the username lowercase
        userLoginDto.Username = userLoginDto.Username.ToLower();

        // Find the user
        User? user = await charwikiDbContext.Users.FirstOrDefaultAsync(e => e.Username == userLoginDto.Username);

        OperationResultWithReturnData<User?> failedResult = new()
        {
            HasFailed = true,
            InternalMessage = "Invalid username or password",
            UserMessage = "Invalid username or password"
        };

        // // If the user does not exist, return unauthorized
        if (user == null)
        {
            return failedResult;
        }

        // If the user does not have a password, return unauthorized
        if (user.PasswordHash == null)
        {
            return failedResult;
        }

        // If the hash version is outdated, get the corresponding password hashing service to check the password and rehash it
        if (user.PasswordHashVersion < passwordHashVersionHistoryService.GetLatestVersion())
        {
            IPasswordHashingService oldPasswordHashingService = passwordHashVersionHistoryService.GetPasswordHashingServiceForVersion(user.PasswordHashVersion);

            // If the password is incorrect, return unauthorized
            if (!oldPasswordHashingService.VerifyPassword(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return failedResult;
            }

            // Rehash the password
            var (hashedPassword, newSalt) = passwordHashingService.HashPassword(userLoginDto.Password);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = newSalt;
            user.PasswordHashVersion = passwordHashVersionHistoryService.GetLatestVersion();
            await charwikiDbContext.SaveChangesAsync();
        }

        // If the password is incorrect, return unauthorized
        if (!passwordHashingService.VerifyPassword(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return failedResult;
        }

        return new OperationResultWithReturnData<User?>
        {
            HasFailed = false,
            InternalMessage = "Login successful",
            UserMessage = "Login successful",
            ReturnData = user
        };
    }
}