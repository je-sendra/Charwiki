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
    /// The unique identifier of the Loomian the set is for.
    /// </summary>
    public Guid LoomianId { get; set; }

    /// <summary>
    /// The Loomian of the set.
    /// </summary>
    public virtual Loomian? Loomian { get; set; }

    /// <summary>
    /// The unique identifier of the ability of the Loomian in the set.
    /// </summary>
    public required Guid LoomianAbilityId { get; set; }

    /// <summary>
    /// The ability of the Loomian in the set.
    /// </summary>
    public virtual LoomianAbility? Ability { get; set; }

    /// <summary>
    /// The unique identifier of the item the Loomian is holding.
    /// </summary>
    public Guid? ItemId { get; set; }

    /// <summary>
    /// The item the Loomian is holding.
    /// </summary>
    public virtual LoomianItem? Item { get; set; }

    #region Stat Assignments

    /// <summary>
    /// The unique identifier of the personality modifiers of the Loomian set.
    /// </summary>
    public Guid? PersonalityModifiersId { get; set; }

    /// <summary>
    /// The personality of the Loomian in the set.
    /// </summary>
    public StatsSet? PersonalityModifiers { get; set; }

    /// <summary>
    /// The unique identifier of the training points of the Loomian set.
    /// </summary>
    public Guid? TrainingPointsId { get; set; }

    /// <summary>
    /// The training points of the Loomian set.
    /// </summary>
    public virtual StatsSet? TrainingPoints { get; set; }

    /// <summary>
    /// The unique identifier of the unique points of the Loomian set.
    /// </summary>
    public Guid? UniquePointsId { get; set; }

    /// <summary>
    /// The unique points of the Loomian set.
    /// </summary>
    public virtual StatsSet? UniquePoints { get; set; }
    #endregion

    #region Moveset
    /// <summary>
    /// The unique identifier of the first move of the Loomian.
    /// </summary>
    public Guid? Move1Id { get; set; }

    /// <summary>
    /// The first move of the Loomian.
    /// </summary>
    public virtual LoomianMove? Move1 { get; set; }

    /// <summary>
    /// The unique identifier of the second move of the Loomian.
    /// </summary>
    public Guid? Move2Id { get; set; }

    /// <summary>
    /// The second move of the Loomian.
    /// </summary>
    public virtual LoomianMove? Move2 { get; set; }

    /// <summary>
    /// The unique identifier of the third move of the Loomian.
    /// </summary>
    public Guid? Move3Id { get; set; }

    /// <summary>
    /// The third move of the Loomian.
    /// </summary>
    public virtual LoomianMove? Move3 { get; set; }

    /// <summary>
    /// The unique identifier of the fourth move of the Loomian.
    /// </summary>
    public Guid? Move4Id { get; set; }

    /// <summary>
    /// The fourth move of the Loomian.
    /// </summary>
    public virtual LoomianMove? Move4 { get; set; }
    #endregion

    #region Descriptive Fields
    /// <summary>
    /// The title of the set.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// A short description of the Loomian set.
    /// This is typically a brief summary or tagline that captures the essence of the set.
    /// </summary>
    public string ShortDescription { get; set; } = string.Empty;

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
    /// The unique identifier of the game version the set is for.
    /// </summary>
    public required Guid GameVersionInfoId { get; set; }

    /// <summary>
    /// The game version the set is for.
    /// </summary>
    public virtual GameVersionInfo? GameVersionInfo { get; set; }
    #endregion

    #region Metadata
    /// <summary>
    /// The timestamp of the creation of the set.
    /// </summary>
    public DateTime CreationTimestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The unique identifier of the user who created the set.
    /// </summary>
    public required Guid CreatorId { get; set; }

    /// <summary>
    /// The user who created the set.
    /// </summary>
    public virtual User? Creator { get; set; }

    /// <summary>
    /// Whether the set has been approved by a moderator.
    /// </summary>
    public bool Approved { get; set; } = false;

    /// <summary>
    /// The unique identifier of the user who approved the set.
    /// </summary>
    public Guid? ApproverId { get; set; }

    /// <summary>
    /// The user who approved the set.
    /// </summary>
    public virtual User? Approver { get; set; }

    /// <summary>
    /// The timestamp of when the set was approved.
    /// </summary>
    public DateTime? ApprovalTimestamp { get; set; }

    /// <summary>
    /// User ratings for this Loomian set.
    /// </summary>
    public virtual List<UserToLoomianSetStarRating>? UserToLoomianSetStarRatings { get; set; }
    #endregion
}