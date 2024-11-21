using Market.Models;

namespace Market.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid id);
    Task<Product> CreateProductAsync(Dtos.ProductDtos.ProductCreateDto product);
    Task<Product> UpdateProductAsync(Guid id, Dtos.ProductDtos.ProductUpdateDto product);
    Task<bool> DeleteProductAsync(Guid id);
}