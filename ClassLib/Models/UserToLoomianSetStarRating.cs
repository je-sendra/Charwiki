namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents a star rating given by a user to a Loomian set.
/// This is used to allow users to rate Loomian sets, which can be useful for sharing
/// and discovering high-quality sets.
/// </summary>
public class UserToLoomianSetStarRating
{
    /// <summary>
    /// The id of the user who placed the star rating.
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// The id of the Loomian set that was rated.
    /// </summary>
    public required Guid LoomianSetId { get; set; }

    /// <summary>
    /// Gets or sets the star rating.
    /// </summary>
    public int StarRating { get; set; }
}