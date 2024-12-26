namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for hashing passwords.
/// </summary>
public interface IPasswordHashingService
{
    /// <summary>
    /// Hashes a password.
    /// </summary>
    /// <param name="password"></param>
    /// <returns>A tuple with the HashedPassword and salt</returns>
    (string HashedPassword, string? Salt) HashPassword(string password);

    /// <summary>
    /// Verifies a password against a hash.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="hash"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    bool VerifyPassword(string password, string hash, string? salt);
}