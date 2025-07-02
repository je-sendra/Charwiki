namespace Charwiki.ClassLib.Dto.Request;

/// <summary>
/// Represents a request to create a new tag for Loomian sets.
/// Tags can be used to categorize or label Loomian sets for easier identification and organization.
/// </summary>
public class CreateTagRequestDto
{
    /// <summary>
    /// The name of the tag.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The description of the tag.
    /// </summary>
    public string Description { get; set; } = null!;
}