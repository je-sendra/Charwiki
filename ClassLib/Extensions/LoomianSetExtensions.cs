using Charwiki.ClassLib.Dto.Request;
using Charwiki.ClassLib.Exceptions;
using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Extensions;

/// <summary>
/// Contains extension methods for Loomian sets.
/// </summary>
public static class LoomianSetExtensions
{
    /// <summary>
    /// Ensures that a Loomian set is valid. Performs all checks.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static OperationResult ValidateSet(this SubmitLoomianSetRequestDto loomianSet)
    {
        OperationResult tpsValidationResult = ValidateTrainingPoints(loomianSet);
        if (tpsValidationResult.HasFailed) return tpsValidationResult;

        OperationResult upsValidationResult = ValidateUniquePoints(loomianSet);
        if (upsValidationResult.HasFailed) return upsValidationResult;

        OperationResult personalityValidationResult = ValidatePersonality(loomianSet);
        if (personalityValidationResult.HasFailed) return personalityValidationResult;

        string message = $"Loomian set {loomianSet.Title} is valid.";
        return new OperationResult
        {
            HasFailed = false,
            InternalMessage = message,
            UserMessage = message
        };
    }

    /// <summary>
    /// Ensures that the training points of a Loomian set are valid.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static OperationResult ValidateTrainingPoints(this SubmitLoomianSetRequestDto loomianSet)
    {
        if (loomianSet.TrainingPoints == null)
        {
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = "Training points cannot be null.",
                UserMessage = "Training points have not been sent to the server."
            };
        }

        CreateStatsSetRequestDto trainingPoints = loomianSet.TrainingPoints;

        // Check if each stat has valid training points
        OperationResult statsValidation = ValidateAllStatsInRange(trainingPoints, 0, 200, "Training Points");
        if (statsValidation.HasFailed) return statsValidation;

        int totalTps = trainingPoints.Health +
                       trainingPoints.Energy +
                       trainingPoints.MeleeAttack +
                       trainingPoints.RangedAttack +
                       trainingPoints.MeleeDefense +
                       trainingPoints.RangedDefense +
                       trainingPoints.Speed;
        if (totalTps > 500)
        {
            string moreThan500Message = "Total training points must not exceed 500.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = moreThan500Message,
                UserMessage = moreThan500Message
            };
        }

        string successMessage = $"Training points for Loomian set {loomianSet.Title} are valid.";
        return new OperationResult
        {
            HasFailed = false,
            InternalMessage = successMessage,
            UserMessage = successMessage
        };

    }

    /// <summary>
    /// Ensures that the unique points of a Loomian set are valid.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static OperationResult ValidateUniquePoints(this SubmitLoomianSetRequestDto loomianSet)
    {
        if (loomianSet.UniquePoints == null)
        {
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = "Unique points cannot be null.",
                UserMessage = "Unique points have not been sent to the server."
            };
        }
        CreateStatsSetRequestDto uniquePoints = loomianSet.UniquePoints;

        // Check if each stat has valid unique points
        OperationResult statsValidation = ValidateAllStatsInRange(uniquePoints, 0, 200, "Unique Points");
        return statsValidation;
    }

    /// <summary>
    /// Ensures that the personality of a Loomian set is valid.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static OperationResult ValidatePersonality(this SubmitLoomianSetRequestDto loomianSet)
    {
        if (loomianSet.PersonalityModifiers == null)
        {
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = "Personality modifiers cannot be null.",
                UserMessage = "Personality modifiers have not been sent to the server."
            };
        }

        CreateStatsSetRequestDto personalityModifiers = loomianSet.PersonalityModifiers;

        int[] allowedModifiers = [-20, -10, 0, 10, 20];
        if (!allowedModifiers.Contains(personalityModifiers.Health) ||
            !allowedModifiers.Contains(personalityModifiers.Energy) ||
            !allowedModifiers.Contains(personalityModifiers.MeleeAttack) ||
            !allowedModifiers.Contains(personalityModifiers.RangedAttack) ||
            !allowedModifiers.Contains(personalityModifiers.MeleeDefense) ||
            !allowedModifiers.Contains(personalityModifiers.RangedDefense) ||
            !allowedModifiers.Contains(personalityModifiers.Speed))
        {
            string notAllowedMessage = "One or more personality modifiers are not valid.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = notAllowedMessage,
                UserMessage = notAllowedMessage
            };
        }


        // Variables for logic checks
        int totalNegativePersonalityTraits = 0;
        int totalPositivePersonalityTraits = 0;
        bool hasVeryNegativePersonalityTrait = false;
        bool hasVeryPositivePersonalityTrait = false;
        CountPersonalityValues(personalityModifiers, ref totalNegativePersonalityTraits, ref totalPositivePersonalityTraits);
        CheckIfPersonalityHasVery(personalityModifiers, ref hasVeryNegativePersonalityTrait, ref hasVeryPositivePersonalityTrait);

        // Perform all logic checks to make sure the personality makes sense
        OperationResult logicChecksResult = PerformPersonalityLogicChecks(totalNegativePersonalityTraits, totalPositivePersonalityTraits, hasVeryNegativePersonalityTrait, hasVeryPositivePersonalityTrait);
        if (logicChecksResult.HasFailed) return logicChecksResult;

        string successMessage = $"Personality for Loomian set {loomianSet.Title} is valid.";
        return new OperationResult
        {
            HasFailed = false,
            InternalMessage = successMessage,
            UserMessage = successMessage
        };
    }

    /// <summary>
    /// Counts the number of positive and negative personality traits in a Loomian set's personality modifiers.
    /// </summary>
    /// <param name="personalityModifiers"></param>
    /// <param name="totalNegativePersonalityTraits"></param>
    /// <param name="totalPositivePersonalityTraits"></param>
    private static void CountPersonalityValues(CreateStatsSetRequestDto personalityModifiers, ref int totalNegativePersonalityTraits, ref int totalPositivePersonalityTraits)
    {
        // Count the number of positive and negative personality traits
        if (personalityModifiers.Health < 0) totalNegativePersonalityTraits++;
        else if (personalityModifiers.Health > 0) totalPositivePersonalityTraits++;
        if (personalityModifiers.Energy < 0) totalNegativePersonalityTraits++;
        else if (personalityModifiers.Energy > 0) totalPositivePersonalityTraits++;
        if (personalityModifiers.MeleeAttack < 0) totalNegativePersonalityTraits++;
        else if (personalityModifiers.MeleeAttack > 0) totalPositivePersonalityTraits++;
        if (personalityModifiers.RangedAttack < 0) totalNegativePersonalityTraits++;
        else if (personalityModifiers.RangedAttack > 0) totalPositivePersonalityTraits++;
        if (personalityModifiers.MeleeDefense < 0) totalNegativePersonalityTraits++;
        else if (personalityModifiers.MeleeDefense > 0) totalPositivePersonalityTraits++;
        if (personalityModifiers.RangedDefense < 0) totalNegativePersonalityTraits++;
        else if (personalityModifiers.RangedDefense > 0) totalPositivePersonalityTraits++;
        if (personalityModifiers.Speed < 0) totalNegativePersonalityTraits++;
        else if (personalityModifiers.Speed > 0) totalPositivePersonalityTraits++;
    }

    /// <summary>
    /// Checks if the personality modifiers have very negative or very positive traits.
    /// </summary>
    /// <param name="personalityModifiers"></param>
    /// <param name="hasVeryNegativePersonalityTrait"></param>
    /// <param name="hasVeryPositivePersonalityTrait"></param>
    private static void CheckIfPersonalityHasVery(CreateStatsSetRequestDto personalityModifiers, ref bool hasVeryNegativePersonalityTrait, ref bool hasVeryPositivePersonalityTrait)
    {
        // Check if there are very negative or very positive personality traits
        if (personalityModifiers.Health == -20 || personalityModifiers.Energy == -20 ||
            personalityModifiers.MeleeAttack == -20 || personalityModifiers.RangedAttack == -20 ||
            personalityModifiers.MeleeDefense == -20 || personalityModifiers.RangedDefense == -20 ||
            personalityModifiers.Speed == -20)
        {
            hasVeryNegativePersonalityTrait = true;
        }
        if (personalityModifiers.Health == 20 || personalityModifiers.Energy == 20 ||
            personalityModifiers.MeleeAttack == 20 || personalityModifiers.RangedAttack == 20 ||
            personalityModifiers.MeleeDefense == 20 || personalityModifiers.RangedDefense == 20 ||
            personalityModifiers.Speed == 20)
        {
            hasVeryPositivePersonalityTrait = true;
        }
    }

    /// <summary>
    /// Validates that all stats in a StatsSet are within a specified range.
    /// </summary>
    /// <param name="stats"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="statType"></param>
    /// <returns></returns>
    private static OperationResult ValidateAllStatsInRange(CreateStatsSetRequestDto stats, int minValue, int maxValue, string statType)
    {
        OperationResult hpValidation = ValidateStatInRange(stats.Health, "Health", minValue, maxValue, statType);
        if (hpValidation.HasFailed) return hpValidation;
        OperationResult energyValidation = ValidateStatInRange(stats.Energy, "Energy", minValue, maxValue, statType);
        if (energyValidation.HasFailed) return energyValidation;
        OperationResult meleeAttackValidation = ValidateStatInRange(stats.MeleeAttack, "Melee Attack", minValue, maxValue, statType);
        if (meleeAttackValidation.HasFailed) return meleeAttackValidation;
        OperationResult rangedAttackValidation = ValidateStatInRange(stats.RangedAttack, "Ranged Attack", minValue, maxValue, statType);
        if (rangedAttackValidation.HasFailed) return rangedAttackValidation;
        OperationResult meleeDefenseValidation = ValidateStatInRange(stats.MeleeDefense, "Melee Defense", minValue, maxValue, statType);
        if (meleeDefenseValidation.HasFailed) return meleeDefenseValidation;
        OperationResult rangedDefenseValidation = ValidateStatInRange(stats.RangedDefense, "Ranged Defense", minValue, maxValue, statType);
        if (rangedDefenseValidation.HasFailed) return rangedDefenseValidation;
        OperationResult speedValidation = ValidateStatInRange(stats.Speed, "Speed", minValue, maxValue, statType);
        if (speedValidation.HasFailed) return speedValidation;

        string successMessage = $"{statType} are valid.";
        return new OperationResult
        {
            HasFailed = false,
            InternalMessage = successMessage,
            UserMessage = successMessage
        };
    }

    /// <summary>
    /// Validates that a stat is within a specified range.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="statName"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="statType"></param>
    /// <returns></returns>
    private static OperationResult ValidateStatInRange(int value, string statName, int minValue, int maxValue, string statType)
    {
        if (value < minValue || value > maxValue)
        {
            string notBetweenRangeMessage = $"T{statType} for {statName} must be between {minValue} and {maxValue}.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = notBetweenRangeMessage,
                UserMessage = notBetweenRangeMessage
            };
        }

        string successMessage = $"{statType} for {statName} are valid.";
        return new OperationResult
        {
            HasFailed = false,
            InternalMessage = successMessage,
            UserMessage = successMessage
        };
    }

    /// <summary>
    /// Performs all logic checks for the personality of a Loomian set.
    /// </summary>
    /// <param name="totalNegativePersonalityTraits"></param>
    /// <param name="totalPositivePersonalityTraits"></param>
    /// <param name="hasVeryNegativePersonalityTrait"></param>
    /// <param name="hasVeryPositivePersonalityTrait"></param>
    /// <exception cref="InvalidSetException"></exception>
    private static OperationResult PerformPersonalityLogicChecks(int totalNegativePersonalityTraits, int totalPositivePersonalityTraits, bool hasVeryNegativePersonalityTrait, bool hasVeryPositivePersonalityTrait)
    {
        // There can't be more than 3 total personality traits
        if (totalNegativePersonalityTraits + totalPositivePersonalityTraits > 3)
        {
            string moreThanThreeMessage = "There can't be more than 3 total personality traits.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = moreThanThreeMessage,
                UserMessage = moreThanThreeMessage
            };
        }

        // There can't be more than 2 negative personality traits
        if (totalNegativePersonalityTraits > 2)
        {
            string moreThanTwoNegativeMessage = "There can't be more than 2 negative personality traits.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = moreThanTwoNegativeMessage,
                UserMessage = moreThanTwoNegativeMessage
            };
        }

        // There can't be more than 2 positive personality traits
        if (totalPositivePersonalityTraits > 2)
        {
            string moreThanTwoPositiveMessage = "There can't be more than 2 positive personality traits.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = moreThanTwoPositiveMessage,
                UserMessage = moreThanTwoPositiveMessage
            };
        }

        // If there is a very negative personality trait, there must be 2 positive personality traits
        if (hasVeryNegativePersonalityTrait && totalPositivePersonalityTraits != 2)
        {
            string veryNegativeMessage = "If there is a very negative personality trait, there must be 2 positive personality traits.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = veryNegativeMessage,
                UserMessage = veryNegativeMessage
            };
        }

        // If there is a very positive personality trait, there must be 2 negative personality traits
        if (hasVeryPositivePersonalityTrait && totalNegativePersonalityTraits != 2)
        {
            string veryPositiveMessage = "If there is a very positive personality trait, there must be 2 negative personality traits.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = veryPositiveMessage,
                UserMessage = veryPositiveMessage
            };
        }

        // There can't be both a very negative and very positive personality trait
        if (hasVeryNegativePersonalityTrait && hasVeryPositivePersonalityTrait)
        {
            string bothVeryMessage = "There can't be both a very negative and very positive personality trait.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = bothVeryMessage,
                UserMessage = bothVeryMessage
            };
        }

        // If there are 3 personality traits, there must be 1 very negative or 1 very positive personality trait
        if (totalNegativePersonalityTraits + totalPositivePersonalityTraits == 3 && !hasVeryNegativePersonalityTrait && !hasVeryPositivePersonalityTrait)
        {
            string threeTraitsMessage = "If there are 3 personality traits, there must be 1 very negative or 1 very positive personality trait.";
            return new OperationResult
            {
                HasFailed = true,
                InternalMessage = threeTraitsMessage,
                UserMessage = threeTraitsMessage
            };
        }

        // If all checks pass, return a success message
        string successMessage = "Personality logic checks passed.";
        return new OperationResult
        {
            HasFailed = false,
            InternalMessage = successMessage,
            UserMessage = successMessage
        };
    }
}