namespace Charwiki.WebApi.Interfaces;

/// <summary>
/// Represents an object that can be saved to a database.
/// </summary>
public interface IDatabaseSaveable
{
    /// <summary>
    /// The unique identifier of the object.
    /// </summary>
    public Guid Id { get; set; }
}