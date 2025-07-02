namespace Charwiki.ClassLib.Dto.QueryParams;

/// <summary>
/// Represents the query parameters for paginated requests.
/// </summary>
public class PaginatedQueryParams
{
    /// <summary>
    /// The page number to retrieve.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; set; }
}