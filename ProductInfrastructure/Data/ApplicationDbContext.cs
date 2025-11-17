using Microsoft.EntityFrameworkCore;
using ProductDomain.Entities;
namespace ProductInfrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    base(options)
    {
    }
    public DbSet<Product> Products => Set<Product>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
    .IsRequired()
    .HasMaxLength(100);
            entity.Property(p => p.Description)
    .HasMaxLength(500);
            entity.Property(p => p.Price)
    .HasColumnType("decimal(18,2)");
            entity.Property(p => p.CreatedAt)
    .IsRequired();
        });
        base.OnModelCreating(modelBuilder);
    }
}