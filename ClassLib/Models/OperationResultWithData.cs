namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents the result of an operation with additional data.
/// This class extends the OperationResult class to include a data property that can hold any type of result.
/// It is used to provide feedback to the caller about the success or failure of an operation, along with any return data.
/// </summary>
/// <typeparam name="T"></typeparam>
public class OperationResultWithReturnData<T> : OperationResult
{
    /// <summary>
    /// The data resulting from the operation.
    /// </summary>
    public T? ReturnData { get; set; }
}