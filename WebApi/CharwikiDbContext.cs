using Charwiki.ClassLib.Models;
using Microsoft.EntityFrameworkCore;

namespace Charwiki.WebApi;

/// <summary>
/// The database context for the Charwiki application.
/// </summary>
public class CharwikiDbContext : DbContext
{
    /// <summary>
    /// The constructor for the database context.
    /// </summary>
    /// <param name="options"></param>
    public CharwikiDbContext(DbContextOptions<CharwikiDbContext> options)
            : base(options)
    {
    }

    /// <summary>
    /// The database table containing all game version information.
    /// </summary>
    public DbSet<GameVersionInfo> GameVersionInfos { get; set; }

    /// <summary>
    /// The database table containing all Loomians.
    /// </summary>
    public DbSet<Loomian> Loomians { get; set; }

    /// <summary>
    /// The database table containing all Loomian abilities.
    /// </summary>
    public DbSet<LoomianAbility> LoomianAbilities { get; set; }

    /// <summary>
    /// The database table containing all Loomian items.
    /// </summary>
    public DbSet<LoomianItem> LoomianItems { get; set; }

    /// <summary>
    /// The database table containing all Loomian moves.
    /// </summary>
    public DbSet<LoomianMove> LoomianMoves { get; set; }

    /// <summary>
    /// The database table containing all Loomian sets.
    /// </summary>
    public DbSet<LoomianSet> LoomianSets { get; set; }

    /// <summary>
    /// The database table containing all value to stat assignments.
    /// </summary>
    public DbSet<ValueToStatAssignment> ValueToStatAssignments { get; set; }
}