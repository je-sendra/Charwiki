namespace Charwiki.ClassLib.Dto.Request;

/// <summary>
/// Request DTO for creating a new Loomian ability.
/// </summary>
public class CreateLoomianAbilityRequestDto
{
    /// <summary>
    /// The name of the Loomian ability.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}