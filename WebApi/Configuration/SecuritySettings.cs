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
}

/// <summary>
/// Represents the algorithms for password hashing.
/// </summary>
public enum PasswordHashingAlgorithm
{
    /// <summary>
    /// BCrypt.
    /// </summary>
    BCrypt
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