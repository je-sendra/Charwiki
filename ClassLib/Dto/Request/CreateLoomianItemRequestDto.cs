namespace Charwiki.ClassLib.Dto.Request;

/// <summary>
/// Represents a request to create a Loomian item.
/// </summary>
public class CreateLoomianItemRequestDto
{
    /// <summary>
    /// The common name of the item.
    /// </summary>
    public required string Name { get; set; }
}