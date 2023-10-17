using System.ComponentModel.DataAnnotations;
using VewTech.Charwiki.Library.Enums;

namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// The main class of the app. Represents the creatures called Loomian.
/// </summary>
public class Loomian
{
    #region Basic Data
    /// <summary>
    /// The unique identifier.
    /// </summary>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// The number the Loomian is assigned in the Loomipedia.
    /// </summary>
    [Required]
    public required int LoomipediaNumber { get; set; }

    /// <summary>
    /// The name of the Loomian.
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// The short description of the loomian. For example, Embit is the Rabbit Loomian
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The route where a valid image for the Loomian is hosted.
    /// </summary>
    public Uri? ImageUri { get; set; }
    #endregion

    #region Base Stats
    /// <summary>
    /// The base health of the Loomian.
    /// </summary>
    [Required]
    public required int Health { get; set; }

    /// <summary>
    /// The base energy of the Loomian.
    /// </summary>
    [Required]
    public required int Energy { get; set; }

    /// <summary>
    /// The base meelee attack of the Loomian.
    /// </summary>
    [Required]
    public required int MeeleeAttack { get; set; }

    /// <summary>
    /// The base meelee defense of the Loomian.
    /// </summary>
    [Required]
    public required int MeeleeDefense { get; set; }

    /// <summary>
    /// The base ranged attack of the Loomian.
    /// </summary>
    [Required]
    public required int RangedAttack { get; set; }

    /// <summary>
    /// The base ranged defense of the Loomian.
    /// </summary>
    [Required]
    public required int RangedDefense { get; set; }

    /// <summary>
    /// The base speed of the Loomian.
    /// </summary>
    [Required]
    public required int Speed { get; set; }
    #endregion

    #region Typing
    /// <summary>
    /// The primary type of the Loomian.
    /// </summary>
    [Required]
    public required LoomianType Type1 { get; set; }

    /// <summary>
    /// The secondary type of the Loomian.
    /// </summary>
    public LoomianType Type2 { get; set; }
    #endregion

    #region Abilities
    [Required, MaxLength(3)]
    public required IEnumerable<LoomianAbility> Abilities { get; set; }
    #endregion

    public List<Set>? Sets { get; set; }
}