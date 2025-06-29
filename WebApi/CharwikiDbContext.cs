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
    /// The method to configure the database context.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure that the username is unique
        modelBuilder.Entity<User>()
            .HasIndex(e => e.Username)
            .IsUnique();

        // Ensure that the Loomian name is unique
        modelBuilder.Entity<Loomian>()
            .HasIndex(e => e.Name)
            .IsUnique();

        // Ensure that the Loomian ability name is unique
        modelBuilder.Entity<LoomianAbility>()
            .HasIndex(e => e.Name)
            .IsUnique();

        // Ensure that the Loomian item name is unique
        modelBuilder.Entity<LoomianItem>()
            .HasIndex(e => e.Name)
            .IsUnique();

        // Ensure that the Loomian move name is unique
        modelBuilder.Entity<LoomianMove>()
            .HasIndex(e => e.Name)
            .IsUnique();

        // Configure the composite key for UserToLoomianSetStarRating
        modelBuilder.Entity<UserToLoomianSetStarRating>()
            .HasKey(e => new { e.UserId, e.LoomianSetId });
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
    /// The database table containing all users.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// The database table containing all value to stat assignments.
    /// </summary>
    public DbSet<ValueToStatAssignment> ValueToStatAssignments { get; set; }

    /// <summary>
    /// The database table containing the star ratings given by users to Loomian sets.
    /// </summary>
    public DbSet<UserToLoomianSetStarRating> UserToLoomianSetStarRatings { get; set; }
}