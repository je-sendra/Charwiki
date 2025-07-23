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
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The base stats of the Loomian.
    /// </summary>
    public StatsSetResponseDto BaseStats { get; set; } = new StatsSetResponseDto();
}