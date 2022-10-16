using EmiratesIslamic.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EmiratesIslamic.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Function> Functions { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Offer> Offers { get; set; }

    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Currency>().HasKey(c => c.Code);
    }
}