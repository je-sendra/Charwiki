using Konscious.Security.Cryptography;

namespace Charwiki.WebApi.Configuration;

/// <summary>
/// Represents the security configuration.
/// </summary>
public class SecuritySettings
{
    /// <summary>
    /// The JWT configuration.
    /// </summary>
    public required JwtSettings JwtSettings { get; set; }

   /// <summary>
   /// The password hashing settings.
   /// </summary>
    public required List<PasswordHashingSettings> PasswordHashingSettings { get; set; }
}

/// <summary>
/// Represents the JWT configuration.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// The secret key.
    /// </summary>
    public required string Secret { get; set; }

    /// <summary>
    /// The issuer.
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// The audience.
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// The expiry minutes.
    /// </summary>
    public required int ExpiryMinutes { get; set; }
}

/// <summary>
/// Represents the password hashing options.
/// </summary>
public class PasswordHashingSettings
{
    /// <summary>
    /// The algorithm.
    /// </summary>
    public required PasswordHashingAlgorithm Algorithm { get; set; }

    /// <summary>
    /// The version of the password hash. This is used to determine if a password hash needs to be rehashed.
    /// </summary>
    public required int Version { get; set; }

    /// <summary>
    /// The BCrypt settings (optional).
    /// </summary>
    public BcryptSettings? BcryptSettings { get; set; }

    /// <summary>
    /// The Argon2id settings (optional).
    /// </summary>
    public Argon2idSettings? Argon2idSettings { get; set; }
}

/// <summary>
/// Represents the algorithms for password hashing.
/// </summary>
public enum PasswordHashingAlgorithm
{
    /// <summary>
    /// BCrypt.
    /// </summary>
    BCrypt,

    /// <summary>
    /// Argon2id
    /// </summary>
    Argon2id
}

/// <summary>
/// Represents the BCrypt settings.
/// </summary>
public class BcryptSettings
{
    /// <summary>
    /// The work factor.
    /// </summary>
    public required int WorkFactor { get; set; }
}

#pragma warning disable S101 // Linter was detecting Argon2id as separate words and suggesting to change it to Argon2Id

/// <summary>
/// Represents the Argon2id settings.
/// </summary>
public class Argon2idSettings
{
    /// <summary>
    /// The size of the hash in bytes.
    /// </summary>
    public required int HashBytesSize { get; set; }

    /// <summary>
    /// The size of the salt in bytes.
    /// </summary>
    public required int SaltBytesSize { get; set; }

    /// <summary>
    /// The number of iterations.
    /// </summary>
    public required int Iterations { get; set; }

    /// <summary>
    /// The memory size.
    /// </summary>
    public required int MemorySize { get; set; }

    /// <summary>
    /// The degree of parallelism.
    /// </summary>
    public required int DegreeOfParallelism { get; set; }
}