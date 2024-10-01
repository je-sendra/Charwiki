namespace Charwiki.ClassLib.Exceptions;

/// <summary>
/// Represents an exception thrown when a Loomian set is invalid.
/// </summary>
public class InvalidSetException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSetException"/> class.
    /// </summary>
    /// <param name="message"></param>
    public InvalidSetException(string message) : base(message)
    {
    }
}