using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a Loomian.
/// </summary>
public class LoomianResponseDto
{
    /// <summary>
    /// The unique identifier of the Loomian.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the Loomian.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Default constructor with empty properties for serialization purposes.
    /// </summary>
    public LoomianResponseDto()
    {
        Id = Guid.Empty;
        Name = string.Empty;
    }
    
    /// <summary>
    /// Constructor to create a LoomianResponseDto from a Loomian model.
    /// </summary>
    /// <param name="loomian"></param>
    public LoomianResponseDto(Loomian loomian)
    {
        Id = loomian.Id;
        Name = loomian.Name;
    }
}