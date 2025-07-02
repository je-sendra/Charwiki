using Charwiki.ClassLib.Models;

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

    /// <summary>
    /// Default constructor with empty properties for serialization purposes.
    /// </summary>
    public StarRatingResponseDto()
    {
        Stars = 0;
    }

    /// <summary>
    /// Constructor to create a StarRatingResponseDto from a UserToLoomianSetStarRating model.
    /// </summary>
    /// <param name="rating">The UserToLoomianSetStarRating model to convert.</param>
    public StarRatingResponseDto(UserToLoomianSetStarRating rating)
    {
        Stars = rating.StarRating;
    }
}