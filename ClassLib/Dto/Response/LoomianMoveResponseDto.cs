using Charwiki.ClassLib.Enums;

namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a Loomian move.
/// </summary>
public class LoomianMoveResponseDto
{
    /// <summary>
    /// The unique identifier of the Loomian move.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the Loomian move.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The type of the Loomian move.
    /// </summary>
    public LoomianType Type { get; set; }
}