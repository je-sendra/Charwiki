namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for hashing passwords using BCrypt.
/// </summary>
/// <param name="workfactor"></param>
public class BcryptPasswordHashingService(int workfactor) : IPasswordHashingService
{
    /// <inheritdoc/>
    public (string HashedPassword, string? Salt) HashPassword(string password)
    {
        // BCrypt has the salt built in and doesn't need a separate salt
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workfactor);
        return (hashedPassword, null);
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string password, string hash, string? salt)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}