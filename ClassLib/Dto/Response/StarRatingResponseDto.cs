namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves their rating for a Loomian set.
/// </summary>
public class StarRatingResponseDto
{
    /// <summary>
    /// The number of stars given in the rating.
    /// </summary>
    public int Stars { get; set; }
}