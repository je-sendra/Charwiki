namespace Charwiki.ClassLib.Enums
{
    /// <summary>
    /// Represents the roles of a user.
    /// </summary>
    [Flags]
    public enum UserRoles
    {
        /// <summary>
        /// Represents a user with no special permissions.
        /// </summary>
        None,

        /// <summary>
        /// Represents a user that has moderator privileges.
        /// </summary>
        Moderator,
        
        /// <summary>
        /// Represents a user with administrative permissions.
        /// </summary>
        Admin
    }
}