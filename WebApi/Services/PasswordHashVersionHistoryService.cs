using System.Text.Json;
using Charwiki.WebApi.Configuration;
using Microsoft.Extensions.Options;

namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for keeping a history of password hash versions.
/// </summary>
/// <param name="securitySettings"></param>
public class PasswordHashVersionHistoryService(IOptions<SecuritySettings> securitySettings) : IPasswordHashVersionHistoryService
{
    /// <inheritdoc/>
    public int GetLatestVersion()
    {
        return securitySettings.Value.PasswordHashingSettings.Max(x => x.Version);
    }

    /// <inheritdoc/>
    public IPasswordHashingService GetPasswordHashingServiceForVersion(int version)
    {
        PasswordHashingSettings passwordHashingSetting = securitySettings.Value.PasswordHashingSettings.First(x => x.Version == version);
        
        switch (passwordHashingSetting.Algorithm)
        {
            case PasswordHashingAlgorithm.BCrypt:
                if (passwordHashingSetting.BcryptSettings == null)
                {
                    throw new InvalidOperationException("BCrypt settings are required for BCrypt hashing");
                }
                return new BcryptPasswordHashingService(passwordHashingSetting.BcryptSettings.WorkFactor);
            default:
                throw new InvalidOperationException("Unsupported password hashing algorithm");
        }
    }

    /// <inheritdoc/>
    public bool HistoryContainsVersion(int version)
    {
        return securitySettings.Value.PasswordHashingSettings.Any(x => x.Version == version);
    }
}