using Market.Dtos.ProductDtos;
using Market.Interfaces;
using Market.Mappers;
using Microsoft.AspNetCore.Mvc;
using Market.Validators;

namespace Market.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService, 
                            ILogger<ProductsController> logger, 
                            ProductCreateDtoValidator productValidator,
                            ProductUpdateDtoValidator productUpdateValidator ) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
    {
        logger.LogInformation("Received request to get all products");

        try
        {
            var products = await productService.GetAllProductsAsync();
            logger.LogInformation("Products are returned or if there is no any item, [] is returned");

            return Ok(products.Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
                ModifiedAt = p.ModifiedAt,
                Status = (EProductStatus)p.Status
            }));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting all products.");
            return new StatusCodeResult(500);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetProductById(Guid id)
    {
        logger.LogInformation("Received request to get item with id {Id}", id);
        try
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                logger.LogError("Item with id {Id} is not found", id);
                return NotFound();
            }

            logger.LogInformation("Item is found and returned to user");
            return Ok(new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                ModifiedAt = product.ModifiedAt,
                Status = (EProductStatus)product.Status
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting a product.");
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> CreateProduct(ProductCreateDto dto)
    {
        logger.LogInformation("New product is received. Product name {Name}, Product {Price}, Product status{Status}", dto.Name, dto.Price, dto.Status);

        var validationResult = await productValidator.ValidateAsync(dto);

        if (validationResult.IsValid is not true)
        {
            logger.LogWarning("Validation failed: {errors}", validationResult.Errors);
            return new StatusCodeResult(500);
        }

        var product = new ProductCreateDto
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.Status
        };

        try
        {
            var createdProduct = await productService.CreateProductAsync(product);

            logger.LogInformation("Product {name} is successfully created", createdProduct.Name);

            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, new ProductReadDto
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                CreatedAt = createdProduct.CreatedAt,
                ModifiedAt = createdProduct.ModifiedAt,
                Status = (EProductStatus)createdProduct.Status
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while creating a product.");
            return new StatusCodeResult(500);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductReadDto>> UpdateProduct(Guid id, ProductUpdateDto dto)
    {
        logger.LogInformation("Reveived request to update product with Id: {Id}", id);

        var validationResult = await productUpdateValidator.ValidateAsync(dto);

        if (validationResult.IsValid is not true)
        {
            logger.LogWarning("Validation failed: {errors}", validationResult.Errors);
            return new StatusCodeResult(500);
        }

        var productUpdateDto = new ProductUpdateDto
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.Status
        };

        try
        {
            var updatedProductModel = await productService.UpdateProductAsync(id, productUpdateDto);

            var updatedProductDto = updatedProductModel.ToReadDto();

            if (updatedProductDto == null)
            {
                logger.LogError("item with id {id} is not found", id);
                return NotFound();
            } 

            logger.LogInformation("Item with {id} is updated successfully ", updatedProductDto.Id);

            return Ok(new ProductReadDto
            {
                Id = updatedProductDto.Id,
                Name = updatedProductDto.Name,
                Price = updatedProductDto.Price,
                CreatedAt = updatedProductDto.CreatedAt,
                ModifiedAt = updatedProductDto.ModifiedAt,
                Status = updatedProductDto.Status
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while updating a product.");
            return new StatusCodeResult(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        logger.LogInformation("Received request to delete item with id {Id}", id);

        var deleted = await productService.DeleteProductAsync(id);
        if (!deleted)
        {
            logger.LogError("Item with id {Id} is not found", id);
            return NotFound();
        } 

        logger.LogInformation("Item with id {Id} is successfully deleted", id);
        return NoContent();
    }
}