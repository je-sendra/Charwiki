using System.ComponentModel.DataAnnotations;

namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// An item a Loomian can hold.
/// </summary>
public class HeldItem
{
    /// <summary>
    /// The unique identifier
    /// </summary>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// The name of the item.
    /// </summary>
    [Required]
    public required string Name { get; set; }
}