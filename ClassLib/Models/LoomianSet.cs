using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Interfaces;

namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents a set a Loomian can use in battle.
/// </summary>
public class LoomianSet : IDatabaseSaveable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The Loomian of the set.
    /// </summary>
    public required Loomian Loomian { get; set; }

    /// <summary>
    /// The personality of the Loomian in the set.
    /// </summary>
    public required List<ValueToStatAssignment> PersonalityModifiers { get; set; }

    /// <summary>
    /// The ability of the Loomian in the set.
    /// </summary>
    public required LoomianAbility Ability { get; set; }

    /// <summary>
    /// The item the Loomian is holding.
    /// </summary>
    public LoomianItem? Item { get; set; }
    
    /// <summary>
    /// The training points of the Loomian.
    /// </summary>
    public required List<ValueToStatAssignment> TrainingPoints { get; set; }

    /// <summary>
    /// The unique points of the Loomian.
    /// </summary>
    public required List<ValueToStatAssignment> UniquePoints { get; set; }

    #region Moveset
    /// <summary>
    /// The first move of the Loomian.
    /// </summary>
    public LoomianMove? Move1 { get; set; }

    /// <summary>
    /// The second move of the Loomian.
    /// </summary>
    public LoomianMove? Move2 { get; set; }

    /// <summary>
    /// The third move of the Loomian.
    /// </summary>
    public LoomianMove? Move3 { get; set; }

    /// <summary>
    /// The fourth move of the Loomian.
    /// </summary>
    public LoomianMove? Move4 { get; set; }
    #endregion

    #region Descriptive Fields
    /// <summary>
    /// The title of the set.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// The explanation of the set.
    /// </summary>
    public string? Explanation { get; set; }

    /// <summary>
    /// The strategy of the set.
    /// </summary>
    public string? Strategy { get; set; }

    /// <summary>
    /// The strengths of the set.
    /// </summary>
    public List<string>? Strengths { get; set; }

    /// <summary>
    /// The weaknesses of the set.
    /// </summary>
    public List<string>? Weaknesses { get; set; }

    /// <summary>
    /// Other options for the set.
    /// </summary>
    public string? OtherOptions { get; set; }
    #endregion

    #region Game Info
    /// <summary>
    /// The game version the set is for.
    /// </summary>
    public required GameVersionInfo GameVersionInfo { get; set; }
    #endregion

    #region Metadata
    /// <summary>
    /// The timestamp of the creation of the set.
    /// </summary>
    public DateTime CreationTimestamp { get; set; } = DateTime.Now;

    /// <summary>
    /// The user who created the set.
    /// </summary>
    public required User Creator { get; set; }
    #endregion
}