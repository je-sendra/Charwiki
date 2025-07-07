namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a Loomian ability.
/// </summary>
public class LoomianAbilityResponseDto
{
    /// <summary>
    /// The unique identifier of the Loomian ability.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the Loomian ability.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}