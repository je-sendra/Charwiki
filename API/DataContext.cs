using Microsoft.EntityFrameworkCore;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<HeldItem> HeldItems { get; set; }

    public DbSet<Loomian> Loomians { get; set; }

    public DbSet<LoomianAbility> LoomianAbilities { get; set; }

    public DbSet<Move> Moves { get; set; }
}