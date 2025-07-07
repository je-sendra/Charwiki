using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.WebApi.Models;

namespace Charwiki.WebApi.Extensions;

/// <summary>
/// Extensions for converting between LoomianSet entities and DTOs.
/// </summary>
public static class LoomianSetExtensions
{
    /// <summary>
    /// Converts a LoomianSet entity to a LoomianSetResponseDto.
    /// </summary>
    /// <param name="loomianSet">The LoomianSet entity to convert.</param>
    /// <returns>A LoomianSetResponseDto representing the LoomianSet.</returns>
    public static LoomianSetResponseDto ToResponseDto(this LoomianSet loomianSet)
    {
        LoomianSetResponseDto responseDto = new()
        {
            Id = loomianSet.Id,
            Loomian = loomianSet.Loomian?.ToResponseDto(),
            Ability = loomianSet.Ability?.ToResponseDto(),
            Item = loomianSet.Item?.ToResponseDto(),

            Move1 = loomianSet.Move1?.ToResponseDto(),
            Move2 = loomianSet.Move2?.ToResponseDto(),
            Move3 = loomianSet.Move3?.ToResponseDto(),
            Move4 = loomianSet.Move4?.ToResponseDto(),

            Title = loomianSet.Title,
            ShortDescription = loomianSet.ShortDescription,
            Explanation = loomianSet.Explanation,
            Strategy = loomianSet.Strategy,
            Strengths = loomianSet.Strengths,
            Weaknesses = loomianSet.Weaknesses,
            OtherOptions = loomianSet.OtherOptions,

            GameVersionInfo = loomianSet.GameVersionInfo?.ToResponseDto(),
            IsApproved = loomianSet.Approved,

            TrainingPoints = loomianSet.TrainingPoints?.ToResponseDto(),
            UniquePoints = loomianSet.UniquePoints?.ToResponseDto(),
            PersonalityModifiers = loomianSet.PersonalityModifiers?.ToResponseDto(),
        };

        if (loomianSet.Creator != null)
        {
            responseDto.Author = loomianSet.Creator.ToResponseDto();
            responseDto.CreatedAt = loomianSet.CreationTimestamp;
        }

        if (loomianSet.Approver != null)
        {
            responseDto.Approver = loomianSet.Approver.ToResponseDto();
            responseDto.ApprovedAt = loomianSet.ApprovalTimestamp;
        }

        if (loomianSet.UserToLoomianSetStarRatings != null && loomianSet.UserToLoomianSetStarRatings.Any())
        {
            responseDto.AverageRating = loomianSet.UserToLoomianSetStarRatings
                .Average(rating => rating.StarRating);

            responseDto.RatingsCount = loomianSet.UserToLoomianSetStarRatings.Count;
        }

        if (loomianSet.Tags != null)
        {
            responseDto.Tags = loomianSet.Tags
                .Where(t => t.Tag != null)
                .Select(t => t.Tag!.ToResponseDto());
        }
        return responseDto;
    }

    /// <summary>
    /// Converts the DTO to a Loomian set object.
    /// </summary>
    /// <returns></returns>
    public static LoomianSet ToLoomianSet(this SubmitLoomianSetRequestDto requestDto)
    {
        return new LoomianSet
        {
            LoomianId = requestDto.LoomianId.Value,
            PersonalityModifiers = requestDto.PersonalityModifiers.ToEntity(),
            LoomianAbilityId = requestDto.AbilityId.Value,
            ItemId = requestDto.ItemId,
            TrainingPoints = requestDto.TrainingPoints.ToEntity(),
            UniquePoints = requestDto.UniquePoints.ToEntity(),
            Move1Id = requestDto.Move1Id,
            Move2Id = requestDto.Move2Id,
            Move3Id = requestDto.Move3Id,
            Move4Id = requestDto.Move4Id,
            Title = requestDto.Title,
            ShortDescription = requestDto.ShortDescription,
            Explanation = requestDto.Explanation,
            Strategy = requestDto.Strategy,
            Strengths = requestDto.Strengths,
            Weaknesses = requestDto.Weaknesses,
            OtherOptions = requestDto.OtherOptions,
            GameVersionInfoId = requestDto.GameVersionInfoId ?? Guid.Empty
        };
    }
}