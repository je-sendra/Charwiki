namespace Charwiki.ClassLib.Models.OperationResult;

/// <summary>
/// Represents the result of an operation.
/// This class contains properties to indicate whether the operation has failed and a message describing the result.
/// It is used to provide feedback to the caller about the success or failure of an operation.
/// </summary>
public class OperationResult
{
    /// <summary>
    /// Indicates whether the operation has failed or not.
    /// </summary>
    public bool HasFailed { get; set; }

    /// <summary>
    /// A message describing the result of the operation.
    /// This message can provide additional information about the failure or success of the operation.
    /// </summary>
    public string InternalMessage { get; set; } = string.Empty;

    /// <summary>
    /// A user-friendly message that can be displayed to the user.
    /// This message is intended for end-users and should be clear and understandable.
    /// It may be different from the internal message to avoid exposing technical details.
    /// </summary>
    public string UserMessage { get; set; } = string.Empty;
}