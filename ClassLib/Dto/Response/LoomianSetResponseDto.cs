using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a Loomian Set.
/// </summary>
public class LoomianSetResponseDto
{
    #region Basic Properties
    /// <summary>
    /// The unique identifier of the Loomian set.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The Loomian the set is for.
    /// </summary>
    public LoomianResponseDto? Loomian { get; set; } = null!;

    /// <summary>
    /// The ability of the Loomian in the set.
    /// </summary>
    public LoomianAbilityResponseDto? Ability { get; set; } = null!;

    /// <summary>
    /// The item equipped by the Loomian in the set.
    /// </summary>
    public LoomianItemResponseDto? Item { get; set; } = null!;
    #endregion

    #region Moveset
    /// <summary>
    /// The first move in the Loomian's moveset.
    /// </summary>
    public LoomianMoveResponseDto? Move1 { get; set; } = null!;

    /// <summary>
    /// The second move in the Loomian's moveset.
    /// </summary>
    public LoomianMoveResponseDto? Move2 { get; set; } = null!;

    /// <summary>
    /// The third move in the Loomian's moveset.
    /// </summary>
    public LoomianMoveResponseDto? Move3 { get; set; } = null!;

    /// <summary>
    /// The fourth move in the Loomian's moveset.
    /// </summary>
    public LoomianMoveResponseDto? Move4 { get; set; } = null!;
    #endregion

    #region Descriptive Fields
    /// <summary>
    /// The title of the Loomian set.
    /// </summary>
    public string Title { get; set; } = string.Empty;

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

    #region Metadata
    /// <summary>
    /// The version of the game this Loomian set was designed for.
    /// </summary>
    public GameVersionInfoResponseDto? GameVersionInfo { get; set; } = null!;

    /// <summary>
    /// Whether the Loomian set is approved by the Charwiki team.
    /// This is used to indicate if the set has been reviewed and accepted for public use.
    /// </summary>
    public bool IsApproved { get; set; } = false;
    
    /// <summary>
    /// The average rating of the Loomian set.
    /// This is calculated based on user ratings and reflects the overall quality of the set.
    /// </summary>
    public double AverageRating { get; set; }
    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor with empty properties.
    /// </summary>
    public LoomianSetResponseDto()
    {

    }

    /// <summary>
    /// Constructor to create a LoomianSetResponseDto from a LoomianSet model.
    /// </summary>
    /// <param name="loomianSet"></param>
    public LoomianSetResponseDto(LoomianSet loomianSet)
    {
        Id = loomianSet.Id;
        Loomian = loomianSet.Loomian != null ? new LoomianResponseDto(loomianSet.Loomian) : null;
        Ability = loomianSet.Ability != null ? new LoomianAbilityResponseDto(loomianSet.Ability) : null;
        Item = loomianSet.Item != null ? new LoomianItemResponseDto(loomianSet.Item) : null;

        Move1 = loomianSet.Move1 != null ? new LoomianMoveResponseDto(loomianSet.Move1) : null;
        Move2 = loomianSet.Move2 != null ? new LoomianMoveResponseDto(loomianSet.Move2) : null;
        Move3 = loomianSet.Move3 != null ? new LoomianMoveResponseDto(loomianSet.Move3) : null;
        Move4 = loomianSet.Move4 != null ? new LoomianMoveResponseDto(loomianSet.Move4) : null;

        Title = loomianSet.Title;
        Explanation = loomianSet.Explanation;
        Strategy = loomianSet.Strategy;
        Strengths = loomianSet.Strengths;
        Weaknesses = loomianSet.Weaknesses;
        OtherOptions = loomianSet.OtherOptions;

        GameVersionInfo = loomianSet.GameVersionInfo != null ? new GameVersionInfoResponseDto(loomianSet.GameVersionInfo) : null;
        IsApproved = loomianSet.Approved;

        if (loomianSet.UserToLoomianSetStarRatings != null && loomianSet.UserToLoomianSetStarRatings.Any())
        {
            AverageRating = loomianSet.UserToLoomianSetStarRatings
                .Average(rating => rating.StarRating);
        }
    }
    #endregion
}