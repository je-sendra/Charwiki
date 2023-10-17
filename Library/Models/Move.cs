using System.ComponentModel.DataAnnotations;
using VewTech.Charwiki.Library.Enums;
using VewTech.Charwiki.Library.Helpers;
using VewTech.Charwiki.Library.Interfaces;

namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// A move a Loomian can use.
/// </summary>
public class Move : IApiModel
{
    /// <inheritdoc />
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

    
    public static ApiHelper<Move> ApiHelper { get; } = new("/moves");
}