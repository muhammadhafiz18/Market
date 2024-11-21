using Market.Data;
using Market.Interfaces;
using Market.Models;
using Microsoft.EntityFrameworkCore;
using Market.Mappers;

namespace Market.Services;

public class ProductService(IProductDbContext dbContext) : IProductService
{
    public async Task<Product> CreateProductAsync(Dtos.ProductDtos.ProductCreateDto productCreateDto)
    {
        var newProductModel = productCreateDto.ToModel();

        newProductModel.Id = Guid.NewGuid();
        newProductModel.CreatedAt = DateTime.UtcNow;
        newProductModel.ModifiedAt = DateTime.UtcNow;

        dbContext.Products.Add(newProductModel.ToEntity());
        await dbContext.SaveChangesAsync();
        return newProductModel;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var productEntity = await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        var productModel = productEntity.ToModel();

        if (productModel == null) return false;

        dbContext.Products.Remove(productModel.ToEntity());
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    { 
        var productsEntityList = await dbContext.Products
        .Include(p => p.ProductDetail)
        .ToListAsync();

        var productsModelList = productsEntityList
            .Select(p => p.ToModel())
            .ToList();

        return productsModelList;
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var productEntity = await dbContext.Products
        .Include(p => p.ProductDetail)
        .FirstOrDefaultAsync(p => p.Id == id);

        if (productEntity is not null)
        {
            var productModel = productEntity.ToModel();

            return productModel;
        }

        return new Product();
    }

    public async Task<Product> UpdateProductAsync(Guid id, Dtos.ProductDtos.ProductUpdateDto productUpdateDto)
    {
        var existingProductEntity = await dbContext.Products.FindAsync(id);
        
        if (existingProductEntity == null) return null;

        existingProductEntity.Name = productUpdateDto.Name;
        existingProductEntity.Price = productUpdateDto.Price;
        existingProductEntity.ModifiedAt = DateTime.UtcNow;
        existingProductEntity.Status = productUpdateDto.Status;

        await dbContext.SaveChangesAsync();
        return existingProductEntity.ToModel();
    }
}