using Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Data;

public class ProductDbContext : DbContext, IProductDbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductDetail> ProductDetails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product konfiguratsiyasi
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.Price)
                  .HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // ProductDetail konfiguratsiyasi
        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description)
                  .HasMaxLength(500);
            entity.HasOne(e => e.Product)
                  .WithOne(p => p.ProductDetail)
                  .HasForeignKey<ProductDetail>(e => e.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
