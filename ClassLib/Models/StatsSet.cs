using Charwiki.ClassLib.Interfaces;

namespace Charwiki.ClassLib.Models;

/// <summary>
/// Represents a set of stats that can be assigned to Loomians.
/// This is used for TPs, UPs, and other stat-related functionalities.
/// </summary>
public class StatsSet : IDatabaseSaveable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// The health of the Loomian.
    /// </summary>
    public int Health { get; set; }

    /// <summary>
    /// The energy of the Loomian.
    /// </summary>
    public int Energy { get; set; }

    /// <summary>
    /// The melee attack of the Loomian.
    /// </summary>
    public int MeleeAttack { get; set; }

    /// <summary>
    /// The ranged attack of the Loomian.
    /// </summary>
    public int RangedAttack { get; set; }

    /// <summary>
    /// The melee defense of the Loomian.
    /// </summary>
    public int MeleeDefense { get; set; }

    /// <summary>
    /// The ranged defense of the Loomian.
    /// </summary>
    public int RangedDefense { get; set; }

    /// <summary>
    /// The speed of the Loomian.
    /// </summary>
    public int Speed { get; set; }
}