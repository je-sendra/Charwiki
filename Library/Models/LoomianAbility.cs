using System.ComponentModel.DataAnnotations;
using VewTech.Charwiki.Library.Helpers;
using VewTech.Charwiki.Library.Interfaces;

namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// An ability a Loomian can have.
/// </summary>
public class LoomianAbility : IApiModel
{
    /// <inheritdoc/>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// The common name of the ability.
    /// </summary>
    [Required]
    public required string Name { get; set; }

    public static ApiHelper<LoomianAbility> ApiHelper { get; } = new("/loomianAbilities");
}