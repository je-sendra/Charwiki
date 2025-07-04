using Charwiki.WebApi.Interfaces;

namespace Charwiki.WebApi.Models;

/// <summary>
/// Represents an ability that a Loomian can have.
/// </summary>
public class LoomianAbility : IDatabaseSaveable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the ability.
    /// </summary>
    public required string Name { get; set; }
}