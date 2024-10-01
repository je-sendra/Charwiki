using Charwiki.ClassLib.Exceptions;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Extensions;

/// <summary>
/// Contains extension methods for <see cref="LoomianSet"/>.
/// </summary>
public static class LoomianSetExtensions
{
    /// <summary>
    /// Ensures that a Loomian set is valid. Performs all checks.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static void EnsureSetIsValid(this LoomianSet loomianSet)
    {
        EnsureTpsAreValid(loomianSet);
        EnsureUpsAreValid(loomianSet);
        EnsurePersonalityIsValid(loomianSet);
    }

    /// <summary>
    /// Ensures that the training points of a Loomian set are valid.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static void EnsureTpsAreValid(this LoomianSet loomianSet)
    {
        var totalTps = 0;
        foreach (var currentTp in loomianSet.TrainingPoints)
        {
            if (currentTp.Value < 0 || currentTp.Value > 200)
            {
                throw new InvalidSetException($"Training points for {currentTp.Stat} must be between 0 and 200.");
            }
            totalTps += currentTp.Value;
        }
        if (totalTps > 500)
        {
            throw new InvalidSetException("Total training points must be less than or equal to 500.");
        }
    }

    /// <summary>
    /// Ensures that the unique points of a Loomian set are valid.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static void EnsureUpsAreValid(this LoomianSet loomianSet)
    {
        foreach (var currentUp in loomianSet.UniquePoints)
        {
            if (currentUp.Value < 0 || currentUp.Value > 40)
            {
                throw new InvalidSetException($"Unique points for {currentUp.Stat} must be between 0 and 40.");
            }
        }
    }

    /// <summary>
    /// Ensures that the personality of a Loomian set is valid.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static void EnsurePersonalityIsValid(this LoomianSet loomianSet)
    {
        // Variables for later logic checks
        var totalNegativePersonalityTraits = 0;
        var totalPositivePersonalityTraits = 0;
        var hasVeryNegativePersonalityTrait = false;
        var hasVeryPositivePersonalityTrait = false;

        foreach (var currentPersonality in loomianSet.PersonalityModifiers)
        {
            // Personality modifiers must be one of the following % values
            var allowedModifiers = new List<int>() { -20, -10, 0, 10, 20 };
            if (!allowedModifiers.Contains(currentPersonality.Value))
            {
                throw new InvalidSetException($"{currentPersonality.Value}% is not a valid personality modifier for {currentPersonality.Stat}.");    
            }

            // Count the number of positive and negative personality traits
            if (currentPersonality.Value < 0)
            {
                totalNegativePersonalityTraits++;
            }
            else if (currentPersonality.Value > 0)
            {
                totalPositivePersonalityTraits++;
            }

            // Check if there are very negative or very positive personality traits
            if (currentPersonality.Value == -20)
            {
                hasVeryNegativePersonalityTrait = true;
            }
            else if (currentPersonality.Value == 20)
            {
                hasVeryPositivePersonalityTrait = true;
            }
        }

        // Perform all logic checks to make sure the personality makes sense
        PerformPersonalityLogicChecks(totalNegativePersonalityTraits, totalPositivePersonalityTraits, hasVeryNegativePersonalityTrait, hasVeryPositivePersonalityTrait);
    }

    /// <summary>
    /// Performs all logic checks for the personality of a Loomian set.
    /// </summary>
    /// <param name="totalNegativePersonalityTraits"></param>
    /// <param name="totalPositivePersonalityTraits"></param>
    /// <param name="hasVeryNegativePersonalityTrait"></param>
    /// <param name="hasVeryPositivePersonalityTrait"></param>
    /// <exception cref="InvalidSetException"></exception>
    private static void PerformPersonalityLogicChecks(int totalNegativePersonalityTraits, int totalPositivePersonalityTraits, bool hasVeryNegativePersonalityTrait, bool hasVeryPositivePersonalityTrait)
    {
        // There can't be more than 3 total personality traits
        if (totalNegativePersonalityTraits + totalPositivePersonalityTraits > 3)
        {
            throw new InvalidSetException("There can't be more than 3 total personality traits.");
        }

        // There can't be more than 2 negative personality traits
        if (totalNegativePersonalityTraits > 2)
        {
            throw new InvalidSetException("There can't be more than 2 negative personality traits.");
        }

        // There can't be more than 2 positive personality traits
        if (totalPositivePersonalityTraits > 2)
        {
            throw new InvalidSetException("There can't be more than 2 positive personality traits.");
        }

        // If there is a very negative personality trait, there must be 2 positive personality traits
        if (hasVeryNegativePersonalityTrait && totalPositivePersonalityTraits != 2)
        {
            throw new InvalidSetException("If there is a very negative personality trait, there must be 2 positive personality traits.");
        }

        // If there is a very positive personality trait, there must be 2 negative personality traits
        if (hasVeryPositivePersonalityTrait && totalNegativePersonalityTraits != 2)
        {
            throw new InvalidSetException("If there is a very positive personality trait, there must be 2 negative personality traits.");
        }

        // There can't be both a very negative and very positive personality trait
        if (hasVeryNegativePersonalityTrait && hasVeryPositivePersonalityTrait)
        {
            throw new InvalidSetException("There can't be both a very negative and very positive personality trait.");
        }
    }
}