using Charwiki.ClassLib.Enums;
using Charwiki.WebApi.Interfaces;

namespace Charwiki.WebApi.Models;

/// <summary>
/// Represents a Loomian in the game.
/// </summary>
public class Loomian : IDatabaseSaveable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The common name of the Loomian.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The primary type of the Loomian.
    /// </summary>
    public required LoomianType Type1 { get; set; }

    /// <summary>
    /// The secondary type of the Loomian.
    /// </summary>
    public LoomianType? Type2 { get; set; }
}