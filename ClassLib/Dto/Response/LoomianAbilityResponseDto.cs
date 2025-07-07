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
    public string Name { get; set; }

    /// <summary>
    /// Default constructor with empty properties for serialization purposes.
    /// </summary>
    public LoomianAbilityResponseDto()
    {
        Id = Guid.Empty;
        Name = string.Empty;
    }

    /// <summary>
    /// Constructor to create a LoomianAbilityResponseDto from a LoomianAbility model.
    /// </summary>
    /// <param name="loomianAbility"></param>
    public LoomianAbilityResponseDto(LoomianAbility loomianAbility)
    {
        Id = loomianAbility.Id;
        Name = loomianAbility.Name;
    }
}