namespace Charwiki.WebApi.Services;

/// <summary>
/// Service for keeping a history of password hash versions.
/// </summary>
public interface IPasswordHashVersionHistoryService
{
    /// <summary>
    /// Gets the latest available version of password hashes in the history.
    /// </summary>
    /// <returns></returns>
    int GetLatestVersion();

    /// <summary>
    /// Gets the password hashing service for the specified version.
    /// </summary>
    /// <param name="version"></param>
    /// <returns></returns>
    IPasswordHashingService GetPasswordHashingServiceForVersion(int version);

    /// <summary>
    /// Determines if the history contains the specified version.
    /// </summary>
    /// <param name="version"></param>
    /// <returns></returns>
    bool HistoryContainsVersion(int version);
}