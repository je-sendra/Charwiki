using Charwiki.ClassLib.Enums;
using Charwiki.WebApi.Interfaces;

namespace Charwiki.WebApi.Models;

/// <summary>
/// Represents a move that a Loomian can learn.
/// </summary>
public class LoomianMove : IDatabaseSaveable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the move.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The type of the move.
    /// </summary>
    public required LoomianType Type { get; set; }
}