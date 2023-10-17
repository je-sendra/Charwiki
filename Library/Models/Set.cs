using System.ComponentModel.DataAnnotations;
using VewTech.Charwiki.Library.Enums;
using VewTech.Charwiki.Library.Helpers;
using VewTech.Charwiki.Library.Interfaces;

namespace VewTech.Charwiki.Library.Models;

/// <summary>
/// A set a Loomian can use.
/// </summary>
public class Set : IApiModel
{
    #region Basic Data
    /// <inheritdoc />
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// The title of the set.
    /// </summary>
    [Required]
    public required string Title { get; set; }

    /// <summary>
    /// The detailed description of the set. Supports markdown.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The version of the game the set was created for.
    /// </summary>
    public GameVersion GameVersion { get; set; }
    #endregion

    #region TPs
    /// <summary>
    /// The health TPs the Loomian has in the set.
    /// </summary>
    public int HealthTP { get; set; }

    /// <summary>
    /// The energy TPs the Loomian has in the set.
    /// </summary>
    public int EnergyTP { get; set; }

    /// <summary>
    /// The meelee attack TPs the Loomian has in the set.
    /// </summary>
    public int MeeleeAttackTP { get; set; }

    /// <summary>
    /// The meelee defense TPs the Loomian has in the set.
    /// </summary>
    public int MeeleeDefenseTP { get; set; }

    /// <summary>
    /// The ranged attack TPs the Loomian has in the set.
    /// </summary>
    public int RangedAttackTP { get; set; }

    /// <summary>
    /// The ranged defense TPs the Loomian has in the set.
    /// </summary>
    public int RangedDefenseTP { get; set; }

    /// <summary>
    /// The speed TPs the Loomian has in the set.
    /// </summary>
    public int SpeedTP { get; set; }
    #endregion

    #region UPs
    /// <summary>
    /// The health UPs the Loomian has in the set.
    /// </summary>
    public int HealthUP { get; set; }

    /// <summary>
    /// The energy UPs the Loomian has in the set.
    /// </summary>
    public int EnergyUP { get; set; }

    /// <summary>
    /// The meelee attack UPs the Loomian has in the set.
    /// </summary>
    public int MeeleeAttackUP { get; set; }

    /// <summary>
    /// The meelee defense UPs the Loomian has in the set.
    /// </summary>
    public int MeeleeDefenseUP { get; set; }

    /// <summary>
    /// The ranged attack UPs the Loomian has in the set.
    /// </summary>
    public int RangedAttackUP { get; set; }

    /// <summary>
    /// The ranged defense UPs the Loomian has in the set.
    /// </summary>
    public int RangedDefenseUP { get; set; }

    /// <summary>
    /// The speed UPs the Loomian has in the set.
    /// </summary>
    public int SpeedUP { get; set; }
    #endregion

    #region Set Data
    /// <summary>
    /// The moveset the Loomian will have in the set.
    /// </summary>
    [Required, MaxLength(4)]
    public required IEnumerable<Move> Moveset { get; set; }

    /// <summary>
    /// The primary personality the Loomian will use in the set.
    /// </summary>
    [Required]
    public required Personality Personality1 { get; set; }

    /// <summary>
    /// The primary personality the Loomian will use in the set.
    /// </summary>
    public Personality Personality2 { get; set; }

    /// <summary>
    /// The primary personality the Loomian will use in the set.
    /// </summary>
    public Personality Personality3 { get; set; }

    /// <summary>
    /// The ability the Loomian will have.
    /// </summary>
    [Required]
    public required LoomianAbility Ability { get; set; }
    #endregion

    [Required]
    public required Guid LoomianId { get; set; }

    public static ApiHelper<Set> ApiHelper { get; } = new("/sets");
}