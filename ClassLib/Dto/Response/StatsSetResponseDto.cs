namespace Charwiki.ClassLib.Dto.Response;

/// <summary>
/// Returned when a user retrieves a Loomian stats set.
/// </summary>
public class StatsSetResponseDto
{
    /// <summary>
    /// The unique identifier of the Stats set.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The health stat of a Loomian.
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// The energy stat of a Loomian.
    /// </summary>
    public int Energy { get; set; }

    /// <summary>
    /// The melee attack stat of a Loomian.
    /// </summary>
    public int MeleeAttack { get; set; }

    /// <summary>
    /// The ranged attack stat of a Loomian.
    /// </summary>
    public int RangedAttack { get; set; }

    /// <summary>
    /// The melee defense stat of a Loomian.
    /// </summary>
    public int MeleeDefense { get; set; }

    /// <summary>
    /// The ranged defense stat of a Loomian.
    /// </summary>
    public int RangedDefense { get; set; }

    /// <summary>
    /// The speed stat of a Loomian.
    /// </summary>
    public int Speed { get; set; }
}