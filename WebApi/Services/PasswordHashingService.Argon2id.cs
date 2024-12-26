using System.Security.Cryptography;
using System.Text;
using Charwiki.WebApi.Configuration;
using Konscious.Security.Cryptography;

namespace Charwiki.WebApi.Services;

#pragma warning disable S101 // Linter was detecting Argon2id as separate words and suggesting to change it to Argon2Id

/// <summary>
/// Service for hashing passwords using Argon2id.
/// </summary>
public class Argon2idPasswordHashingService(Argon2idSettings argon2idSettings) : IPasswordHashingService
{
    /// <inheritdoc/>
    public (string HashedPassword, string? Salt) HashPassword(string password)
    {
        byte[] salt = GenerateSalt();
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
        argon2.Salt = salt;
        argon2.Iterations = argon2idSettings.Iterations;
        argon2.MemorySize = argon2idSettings.MemorySize;
        argon2.DegreeOfParallelism = argon2idSettings.DegreeOfParallelism;

        byte[] hashBytes = argon2.GetBytes(argon2idSettings.HashBytesSize);
        string hashBase64 = Convert.ToBase64String(hashBytes);

        string saltBase64 = Convert.ToBase64String(salt);
        return (hashBase64, saltBase64);
    }
    
    /// <inheritdoc/>
    public bool VerifyPassword(string password, string hash, string? salt)
    {
        byte[] hashBytes = Convert.FromBase64String(hash);

        if (salt == null)
        {
            return false;
        }
        byte[] saltBytes = Convert.FromBase64String(salt);

        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
        argon2.Salt = saltBytes;
        argon2.Iterations = argon2idSettings.Iterations;
        argon2.MemorySize = argon2idSettings.MemorySize;
        argon2.DegreeOfParallelism = argon2idSettings.DegreeOfParallelism;

        byte[] hashBytesToVerify = argon2.GetBytes(argon2idSettings.HashBytesSize);
        return hashBytes.SequenceEqual(hashBytesToVerify);
    }

    /// <summary>
    /// Generates a salt.
    /// </summary>
    /// <returns></returns>
    private byte[] GenerateSalt()
    {
        var salt = new byte[argon2idSettings.SaltBytesSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }
}