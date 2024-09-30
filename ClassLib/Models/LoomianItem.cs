using Charwiki.ClassLib.Interfaces;

namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents an item a Loomian can hold.
/// </summary>
public class LoomianItem : IDatabaseSaveable
{
    /// <summary>
    /// The unique identifier of the item.
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// The common name of the item.
    /// </summary>
    public required string Name { get; set; }
}