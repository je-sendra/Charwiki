using Charwiki.ClassLib.Models;

namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a Loomian item.
/// </summary>
public class LoomianItemResponseDto
{
    /// <summary>
    /// The unique identifier of the Loomian item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the Loomian item.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor to create a LoomianItemResponseDto from a LoomianItem model.
    /// </summary>
    /// <param name="loomianItem"></param>
    public LoomianItemResponseDto(LoomianItem loomianItem)
    {
        Id = loomianItem.Id;
        Name = loomianItem.Name;
    }
}