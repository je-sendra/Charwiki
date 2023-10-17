using Microsoft.EntityFrameworkCore;
using VewTech.Charwiki.Library.Models;

namespace VewTech.Charwiki.API;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<HeldItem> HeldItems { get; set; }
}