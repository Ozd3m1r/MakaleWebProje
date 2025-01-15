using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

public class RepositoryContext : DbContext
{
    public DbSet<MakaleData> MakaleData { get; set; }
    public DbSet<Kategori> Kategori { get; set; }
    public DbSet<Makale> Makale { get; set; }
    public DbSet<Users> User { get; set; }
    public DbSet<UserRole> Role { get; set; }
    public DbSet<MakaleComment> MakaleComment { get; set; }  // İsim düzeltildi

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        optionsBuilder.ConfigureWarnings(warnings =>

            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));



        base.OnConfiguring(optionsBuilder);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



        modelBuilder.Entity<MakaleComment>()

            .HasOne(mc => mc.Users)

            .WithMany(u => u.MakaleComments)

            .HasForeignKey(mc => mc.UserId)

            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MakaleComment>()

            .HasOne(mc => mc.Makale)

            .WithMany(m => m.MakaleComments)

            .HasForeignKey(mc => mc.MakaleId)

            .OnDelete(DeleteBehavior.Cascade);

    }

}
    