namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents a tag associated with a Loomian set.
/// </summary>
public class TagToLoomianSet
{
    /// <summary>
    /// The unique identifier of the Loomian set.
    /// </summary>
    public Guid LoomianSetId { get; set; }

    /// <summary>
    /// The unique identifier of the tag.
    /// </summary>
    public Guid TagId { get; set; }
    
    /// <summary>
    /// The tag entity.
    /// </summary>
    public virtual Tag? Tag { get; set; } = null!;
}