using System.ComponentModel.DataAnnotations;

namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// An ability a Loomian can have.
/// </summary>
public class LoomianAbility
{
    /// <summary>
    /// The unique identifier.
    /// </summary>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// The common name of the ability.
    /// </summary>
    [Required]
    public required string Name { get; set; }
}