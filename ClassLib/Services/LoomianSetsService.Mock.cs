using Charwiki.ClassLib.Dto.Request;
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
            PersonalityModifiers = new List<ValueToStatAssignment>
            {
                new ValueToStatAssignment
                {
                    Value = 1,
                    Stat = LoomianStat.MeleeAttack
                }
            },
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
    public Task<IEnumerable<LoomianSet>> GetAllAsync()
    {
        return Task.FromResult(_mockData.AsEnumerable());
    }

    /// <inheritdoc />
    public Task<LoomianSet> GetByIdAsync(Guid id)
    {
        var foundSet = _mockData.Find(x => x.Id == id);
        if (foundSet == null)
        {
            throw new KeyNotFoundException();
        }
        return Task.FromResult(foundSet);
    }

    /// <inheritdoc />
    public Task<LoomianSet> GetByIdAsync(Guid id, bool includeValueToStatAssignments = false, bool includeRatings = false)
    {
        return GetByIdAsync(id);
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
}