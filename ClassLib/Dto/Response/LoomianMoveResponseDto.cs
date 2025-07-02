using Charwiki.ClassLib.Enums;
using Charwiki.ClassLib.Models;

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
    public string Name { get; set; }

    /// <summary>
    /// The type of the Loomian move.
    /// </summary>
    public LoomianType Type { get; set; }

    /// <summary>
    /// Default constructor for LoomianMoveResponseDto.
    /// </summary>
    public LoomianMoveResponseDto()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Type = LoomianType.Unknown;
    }

    /// <summary>
    /// Constructor to create a LoomianMoveResponseDto from a LoomianMove model.
    /// </summary>
    /// <param name="loomianMove"></param>
    public LoomianMoveResponseDto(LoomianMove loomianMove)
    {
        Id = loomianMove.Id;
        Name = loomianMove.Name;
        Type = loomianMove.Type;
    }
}