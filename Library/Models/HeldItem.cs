using System.ComponentModel.DataAnnotations;
using VewTech.Charwiki.Library.Helpers;
using VewTech.Charwiki.Library.Interfaces;


namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// An item a Loomian can hold.
/// </summary>
public class HeldItem : IApiModel
{
    /// <inheritdoc/>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// The name of the item.
    /// </summary>
    [Required]
    public required string Name { get; set; }

    public static ApiHelper<HeldItem> ApiHelper { get; } = new("/heldItems");
}