namespace Charwiki.ClassLib.Dto.QueryParams;

/// <summary>
/// DTO for query parameters when retrieving Loomian sets.
/// This class allows specifying options for filtering and including additional data in the response.
/// </summary>
public class LoomianSetQueryParams : PaginatedQueryParams
{
    /// <summary>
    /// If true, only Loomian sets that have been approved will be returned.
    /// Approved sets are those that have been reviewed and accepted by the community or moderators.
    /// </summary>
    public bool HideNonApprovedSets { get; set; }

    /// <summary>
    /// If true, the response will include the Loomian's value to stat assignments.
    /// </summary>
    public bool IncludeValueToStatAssignments { get; set; }

    /// <summary>
    /// If true, the response will include the average rating of the Loomian set.
    /// </summary>
    public bool IncludeAverageRating { get; set; }

    /// <summary>
    /// If true, the response will include the Loomian's ability.
    /// </summary>
    public bool IncludeAbility { get; set; }

    /// <summary>
    /// If true, the response will include the Loomian's item.
    /// </summary>
    public bool IncludeItem { get; set; }

    /// <summary>
    /// If true, the response will include the Loomian's moves.
    /// </summary>
    public bool IncludeMoves { get; set; }

    /// <summary>
    /// If true, the response will include the detailed explanation of the Loomian set.
    /// This may include strategy, strengths, weaknesses, and other descriptive information.
    /// </summary>
    public bool IncludeDetailedExplanation { get; set; }
}