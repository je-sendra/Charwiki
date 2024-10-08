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
    /// The BCrypt work factor.
    /// </summary>
    public required int BCryptWorkFactor { get; set; }
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