using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Dto;

/// <summary>
/// Represents a Loomian set in the game.
/// </summary>
public class LoomianSetDto
{
    /// <summary>
    /// The unique identifier of the Loomian the set is for.
    /// </summary>
    public required Guid LoomianId { get; set; }

    /// <summary>
    /// The personality modifiers of the Loomian in the set.
    /// </summary>
    public required List<ValueToStatAssignment> PersonalityModifiers { get; set; }

    /// <summary>
    /// The unique identifier of the ability of the Loomian in the set.
    /// </summary>
    public required Guid AbilityId { get; set; }

    /// <summary>
    /// The unique identifier of the item the Loomian is holding.
    /// </summary>
    public Guid? ItemId { get; set; }

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
    /// The unique identifier of the first move of the Loomian.
    /// </summary>
    public Guid? Move1Id { get; set; }

    /// <summary>
    /// The unique identifier of the second move of the Loomian.
    /// </summary>
    public Guid? Move2Id { get; set; }

    /// <summary>
    /// The unique identifier of the third move of the Loomian.
    /// </summary>
    public Guid? Move3Id { get; set; }

    /// <summary>
    /// The unique identifier of the fourth move of the Loomian.
    /// </summary>
    public Guid? Move4Id { get; set; }
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
    /// The other options of the set.
    /// </summary>
    public string? OtherOptions { get; set; }
    #endregion

    #region Game Info
    /// <summary>
    /// The unique identifier of the game version info of the set.
    /// </summary>
    public required Guid GameVersionInfoId { get; set; }
    #endregion
}