using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Extensions;

/// <summary>
/// Contains extension methods for <see cref="User"/>.
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Returns the user with all sensitive information removed.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static User GetCopyWithoutSensitiveInformation(this User user)
    {
        return new User
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        };
    }
}