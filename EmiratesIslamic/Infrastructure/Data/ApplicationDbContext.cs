using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmiratesIslamic.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
    IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
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

        modelBuilder.Entity<ApplicationUser>(b =>
        {
            b.HasMany(au => au.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<ApplicationRole>(b =>
        {
            b.HasMany(ar => ar.RoleUsers)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        modelBuilder.Entity<ApplicationUserRole>().ToTable("AspNetUserRoles");

        modelBuilder.Entity<ApplicationUserRole>()
            .HasIndex(ur => ur.UserId).IsUnique();
    }
}