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
    /// If true, the response will include the Loomian's basic information.
    /// This includes the Loomian's name, type, and other essential details.
    /// </summary>
    public bool IncludeLoomian { get; set; }

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

    /// <summary>
    /// If true, the response will include metadata about the Loomian set.
    /// Metadata may include creation timestamp, creator information, and approval status.
    /// </summary>
    public bool IncludeMetadata { get; set; } 

    /// <summary>
    /// Converts the query parameters to a query string format.
    /// </summary>
    /// <returns></returns>
    public string ToQueryString()
    {
        List<string> queryParams = [];

        if (HideNonApprovedSets)
        {
            queryParams.Add("hideNonApprovedSets=true");
        }
        if (IncludeValueToStatAssignments)
        {
            queryParams.Add("includeValueToStatAssignments=true");
        }
        if (IncludeAverageRating)
        {
            queryParams.Add("includeAverageRating=true");
        }
        if (IncludeLoomian)
        {
            queryParams.Add("includeLoomian=true");
        }
        if (IncludeAbility)
        {
            queryParams.Add("includeAbility=true");
        }
        if (IncludeItem)
        {
            queryParams.Add("includeItem=true");
        }
        if (IncludeMoves)
        {
            queryParams.Add("includeMoves=true");
        }
        if (IncludeDetailedExplanation)
        {
            queryParams.Add("includeDetailedExplanation=true");
        }
        if (IncludeMetadata)
        {
            queryParams.Add("includeMetadata=true");
        }

        // Add pagination parameters
        if (Page > 0)
        {
            queryParams.Add($"pageNumber={Page}");
        }
        if (PageSize > 0)
        {
            queryParams.Add($"pageSize={PageSize}");
        }

        return queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;
    }
}