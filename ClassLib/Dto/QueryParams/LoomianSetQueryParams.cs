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
    /// If true, the response will include tags associated with the Loomian set.
    /// </summary>
    public bool IncludeTags { get; set; } = false;

    /// <summary>
    /// The unique identifier of the Loomian for which the sets are being queried.
    /// This can be used to filter the sets to only those that pertain to a specific Loomian.
    /// If null, the query will not filter by Loomian ID.
    /// </summary>
    public Guid? LoomianId { get; set; } = null;

    /// <summary>
    /// The unique identifier of the ability for which the sets are being queried.
    /// This can be used to filter the sets to only those that use a specific ability.
    /// If null, the query will not filter by ability ID.
    /// </summary>
    public Guid? AbilityId { get; set; } = null;

    /// <summary>
    /// The unique identifier of the item for which the sets are being queried.
    /// This can be used to filter the sets to only those that use a specific item.
    /// If null, the query will not filter by item ID.
    /// </summary>
    public Guid? ItemId { get; set; } = null;

    /// <summary>
    /// The unique identifier of the move for which the sets are being queried.
    /// This can be used to filter the sets to only those that use a specific move.
    /// If null, the query will not filter by move ID.
    /// </summary>
    public Guid? MoveId { get; set; } = null;

    /// <summary>
    /// The unique identifier of the game version information for which the sets are being queried.
    /// This can be used to filter the sets to only those that are relevant to a specific game version.
    /// If null, the query will not filter by game version information ID.
    /// </summary>
    public Guid? GameVersionInfoId { get; set; } = null;

    /// <summary>
    /// A collection of unique identifiers for tags associated with the Loomian sets.
    /// This can be used to filter the sets to only those that have specific tags.
    /// If null, the query will not filter by tags.
    /// </summary>
    public IEnumerable<Guid>? TagsIds { get; set; } = null;

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
        if (IncludeTags)
        {
            queryParams.Add("includeTags=true");
        }

        // Add filters for specific IDs if they are provided
        if (LoomianId.HasValue)
        {
            queryParams.Add($"loomianId={LoomianId.Value}");
        }
        if (AbilityId.HasValue)
        {
            queryParams.Add($"abilityId={AbilityId.Value}");
        }
        if (ItemId.HasValue)
        {
            queryParams.Add($"itemId={ItemId.Value}");
        }
        if (MoveId.HasValue)
        {
            queryParams.Add($"moveId={MoveId.Value}");
        }
        if (TagsIds != null && TagsIds.Any())
        {
            // Join the tags with commas to create a single query parameter
            queryParams.Add($"tagsIds={string.Join(",", TagsIds)}");
        }

        // Add pagination parameters
        if (PageNumber > 0)
        {
            queryParams.Add($"pageNumber={PageNumber}");
        }
        if (PageSize > 0)
        {
            queryParams.Add($"pageSize={PageSize}");
        }

        return queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;
    }
}