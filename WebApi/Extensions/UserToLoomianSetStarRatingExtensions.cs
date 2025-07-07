using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for Star Ratings on Loomian Sets.
/// </summary>
public static class UserToLoomianSetStarRatingExtensions
{
    /// <summary>
    /// Converts a UserToLoomianSetStarRating entity to a UserToLoomianSetStarRatingResponseDto.
    /// </summary>
    /// <param name="rating">The UserToLoomianSetStarRating entity to convert.</param>
    /// <returns>A UserToLoomianSetStarRatingResponseDto representing the rating.</returns>
    public static StarRatingResponseDto ToResponseDto(this UserToLoomianSetStarRating rating)
    {
        return new StarRatingResponseDto
        {
            Stars = rating.StarRating
        };
    }
}