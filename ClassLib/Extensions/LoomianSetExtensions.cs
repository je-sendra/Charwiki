using Charwiki.ClassLib.Exceptions;
using Charwiki.ClassLib.Models;
using Charwiki.ClassLib.Models.OperationResult;

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
    public static OperationResult ValidateSet(this LoomianSet loomianSet)
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
    public static OperationResult ValidateTrainingPoints(this LoomianSet loomianSet)
    {
        int totalTps = 0;
        foreach (ValueToStatAssignment currentTp in loomianSet.TrainingPoints)
        {
            if (currentTp.Value < 0 || currentTp.Value > 200)
            {
                string notBetweenRangeMessage = $"Training points for {currentTp.Stat} must be between 0 and 200.";
                return new OperationResult
                {
                    HasFailed = true,
                    InternalMessage = notBetweenRangeMessage,
                    UserMessage = notBetweenRangeMessage
                };
            }
            totalTps += currentTp.Value;
        }
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
    public static OperationResult ValidateUniquePoints(this LoomianSet loomianSet)
    {
        foreach (ValueToStatAssignment currentUp in loomianSet.UniquePoints)
        {
            if (currentUp.Value < 0 || currentUp.Value > 40)
            {
                string notBetweenRangeMessage = $"Unique points for {currentUp.Stat} must be between 0 and 40.";
                return new OperationResult
                {
                    HasFailed = true,
                    InternalMessage = notBetweenRangeMessage,
                    UserMessage = notBetweenRangeMessage
                };
            }
        }

        string successMessage = $"Unique points for Loomian set {loomianSet.Title} are valid.";
        return new OperationResult
        {
            HasFailed = false,
            InternalMessage = successMessage,
            UserMessage = successMessage
        };
    }

    /// <summary>
    /// Ensures that the personality of a Loomian set is valid.
    /// </summary>
    /// <param name="loomianSet"></param>
    /// <exception cref="InvalidSetException"></exception>
    public static OperationResult ValidatePersonality(this LoomianSet loomianSet)
    {
        // Variables for later logic checks
        int totalNegativePersonalityTraits = 0;
        int totalPositivePersonalityTraits = 0;
        bool hasVeryNegativePersonalityTrait = false;
        bool hasVeryPositivePersonalityTrait = false;

        foreach (ValueToStatAssignment currentPersonality in loomianSet.PersonalityModifiers)
        {
            // Personality modifiers must be one of the following % values
            int[] allowedModifiers = [-20, -10, 0, 10, 20];
            if (!allowedModifiers.Contains(currentPersonality.Value))
            {
                string notAllowedMessage = $"{currentPersonality.Value}% is not a valid personality modifier for {currentPersonality.Stat}.";
                return new OperationResult
                {
                    HasFailed = true,
                    InternalMessage = notAllowedMessage,
                    UserMessage = notAllowedMessage
                };
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