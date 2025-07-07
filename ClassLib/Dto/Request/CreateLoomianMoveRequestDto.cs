using Charwiki.ClassLib.Enums;

namespace Charwiki.ClassLib.Dto.Request;

/// <summary>
/// Represents a request to create a Loomian move.
/// </summary>
public class CreateLoomianMoveRequestDto
{
    /// <summary>
    /// The name of the move.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The type of the move.
    /// </summary>
    public LoomianType Type { get; set; }
}