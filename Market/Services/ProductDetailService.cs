using Market.Data;
using Market.Interfaces;
using Market.Mappers;
using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Services;

public class ProductDetailService(IProductDbContext dbContext) : IProductDetailService
{
    public async Task<ProductDetail> GetDetailsByProductIdAsync(Guid productId)
    {
        var productDetailEntity = await dbContext.
                                    ProductDetails
                                    .FirstOrDefaultAsync(d => d.ProductId == Guid.Parse(productId.ToString().Trim()));

        return productDetailEntity!.ToModel();
    }

    public async Task<ProductDetail> CreateProductDetailAsync(Guid productId, ProductDetail detail)
    {
        var productDetailEntity = detail.ToEntity();

        productDetailEntity.Id = Guid.NewGuid();
        productDetailEntity.ProductId = productId;

        dbContext.ProductDetails.Add(productDetailEntity);
        await dbContext.SaveChangesAsync();
        return productDetailEntity.ToModel();
    }

    public async Task<ProductDetail> UpdateProductDetailAsync(Guid productId, ProductDetail detail)
    {
        var existingDetailEntity = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);
        if (existingDetailEntity == null) return null!;
        
        existingDetailEntity.Description = detail.Description;
        existingDetailEntity.Color = detail.Color;
        existingDetailEntity.Material = detail.Material;
        existingDetailEntity.Weight = detail.Weight;
        existingDetailEntity.QuantityInStock = detail.QuantityInStock;
        existingDetailEntity.ManufactureDate = detail.ManufactureDate;
        existingDetailEntity.ExpiryDate = detail.ExpiryDate;
        existingDetailEntity.Size = detail.Size;
        existingDetailEntity.Manufacturer = detail.Manufacturer;
        existingDetailEntity.CountryOfOrigin = detail.CountryOfOrigin;

        await dbContext.SaveChangesAsync();
        return existingDetailEntity.ToModel();
    }

    public async Task<bool> DeleteProductDetailAsync(Guid productId)
    {
        var detailEntity = await dbContext.ProductDetails.
                                    AsNoTracking()
                                    .FirstOrDefaultAsync(d => d.ProductId == productId);

        if (detailEntity == null) return false;

        dbContext.ProductDetails.Remove(detailEntity);
        await dbContext.SaveChangesAsync();
        
        return true;
    }
}