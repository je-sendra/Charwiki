using Charwiki.ClassLib.Interfaces;

namespace Charwiki.ClassLib.Models;

/// <summary>
/// Tags can be used to categorize or label Loomian sets for easier identification and organization.
/// </summary>
public class Tag : IDatabaseSaveable
{
    /// <summary>
    /// The unique identifier of the Loomian set tag.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the tag.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The description of the tag.
    /// </summary>
    public string Description { get; set; } = null!;
}