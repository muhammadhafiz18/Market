using Market.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Market.Data;

public interface IProductDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<ProductDetail> ProductDetails { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
