using System.ComponentModel.DataAnnotations;
using VewTech.Charwiki.Library.Enums;

namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// A move a Loomian can use.
/// </summary>
public class Move
{
    /// <summary>
    /// The unique identifier.
    /// </summary>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// The common name of the move.
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// The typing of the move.
    /// </summary>
    [Required]
    public required LoomianType Type { get; set; }
}