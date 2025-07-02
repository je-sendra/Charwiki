using Charwiki.ClassLib.Dto.QueryParams;
using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Dto.Response;
using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Services;

/// <summary>
/// Service for Loomian set-related operations with prefabricated mock data.
/// </summary>
public class MockLoomianSetsService : ILoomianSetsService
{
    /// <summary>
    /// Mock data.
    /// </summary>
    private static List<LoomianSet> _mockData = new()
    {
        new LoomianSet
        {
            Id = Guid.NewGuid(),
            LoomianId = Guid.NewGuid(),
            Explanation = "Whatever explanation",
            PersonalityModifiers = new StatsSet(),
            LoomianAbilityId = Guid.NewGuid(),
            TrainingPoints = new StatsSet(),
            UniquePoints = new StatsSet(),
            Title = "Test Set",
            GameVersionInfoId = Guid.NewGuid(),
            CreatorId = Guid.NewGuid()
        }
    };

    /// <inheritdoc />
    public Task<LoomianSet> CreateNewAsync(LoomianSet model)
    {
        _mockData.Add(model);
        return Task.FromResult(model);
    }

    /// <inheritdoc />
    public Task<IEnumerable<LoomianSetResponseDto>?> GetAllAsync(LoomianSetQueryParams? queryParams = null)
    {
        return Task.FromResult<IEnumerable<LoomianSetResponseDto>?>([]);
    }

    /// <inheritdoc />
    public Task<LoomianSetResponseDto?> GetByIdAsync(Guid id, LoomianSetQueryParams? queryParams = null)
    {
        LoomianSetResponseDto? loomianSet = null;
        return Task.FromResult(loomianSet);
    }

    /// <inheritdoc />
    public Task<LoomianSet> SubmitSetAsync(SubmitLoomianSetRequestDto loomianSet, string authToken)
    {
        LoomianSet item = loomianSet.ToLoomianSet(Guid.NewGuid());
        _mockData.Add(item);
        return Task.FromResult(item);
    }

    /// <inheritdoc />
    public Task RateLoomianSetAsync(Guid loomianSetId, int starRating, string authToken)
    {
        throw new NotImplementedException("Mock service does not support rating Loomian sets.");
    }

    /// <inheritdoc />
    public Task<StarRatingResponseDto?> GetMyRatingAsync(Guid loomianSetId, string authToken)
    {
        throw new NotImplementedException("Mock service does not support retrieving user ratings for Loomian sets.");
    }
}