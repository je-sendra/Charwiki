namespace Charwiki.ClassLib.Dto.Request;

/// <summary>
/// Represents a request to create a Loomian stats set.
/// </summary>
public class CreateStatsSetRequestDto
{
    /// <summary>
    /// Gets or sets the health of the Loomian.
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// Gets or sets the energy of the Loomian.
    /// </summary>
    public int Energy { get; set; }

    /// <summary>
    /// Gets or sets the melee attack of the Loomian.
    /// </summary>
    public int MeleeAttack { get; set; }

    /// <summary>
    /// Gets or sets the ranged attack of the Loomian.
    /// </summary>
    public int RangedAttack { get; set; }

    /// <summary>
    /// Gets or sets the melee defense of the Loomian.
    /// </summary>
    public int MeleeDefense { get; set; }

    /// <summary>
    /// Gets or sets the ranged defense of the Loomian.
    /// </summary>
    public int RangedDefense { get; set; }

    /// <summary>
    /// Gets or sets the speed of the Loomian.
    /// </summary>
    public int Speed { get; set; }
}