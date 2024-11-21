using Microsoft.AspNetCore.Mvc;
using Market.Interfaces;
using Market.Mappers;
using Market.Validators;

namespace Market.Controllers;

[ApiController]
[Route("api/products/{productId}/details")]
public class ProductDetailsController(IProductDetailService productDetailService, 
                            ILogger<ProductDetailsController> logger,
                            ProductDetailCreateDtoValidator productDetailCreateValidator,
                            ProductDetailUpdateDtoValidator productDetailUpdateValidator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(Guid productId, [FromBody] Dtos.ProductDetailDtos.ProductDetailCreateDto detail)
    {
        if (detail == null)
        {
            return BadRequest("Product detail cannot be null.");
        }
        var validationResult = await productDetailCreateValidator.ValidateAsync(detail);

        if (validationResult.IsValid is not true)
        {
            logger.LogWarning("Validation failed: {errors}", validationResult.Errors);
            return new StatusCodeResult(500);
        }

        try
        {
            var createdDetail = await productDetailService.CreateProductDetailAsync(productId, detail.ToModel());
            logger.LogInformation("Product detail is created");
            return CreatedAtAction(nameof(GetProductDetails), new { productId }, createdDetail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Could not create a product");
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProductDetails(Guid productId)
    {
        try
        {
            var detail = await productDetailService.GetDetailsByProductIdAsync(productId);
            logger.LogInformation("access was to db");
            if (detail == null)
            {
                logger.LogError("No detail found for product with ID {productId}", productId);
                return NotFound($"No details found for product with ID {productId}.");
            }
            logger.LogInformation("product details are returned to user");
            return Ok(detail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while finding product details.");
            return new StatusCodeResult(500);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(Guid productId, [FromBody] Dtos.ProductDetailDtos.ProductDetailUpdateDto detail)
    {
        if (detail == null)
        {
            return BadRequest("Product detail cannot be null.");
        }

        var validationResult = await productDetailUpdateValidator.ValidateAsync(detail);

        if (validationResult.IsValid is not true)
        {
            logger.LogWarning("Validation failed: {errors}", validationResult.Errors);
            return new StatusCodeResult(500);
        }

        try
        {
            var updatedDetail = await productDetailService.UpdateProductDetailAsync(productId, detail.ToUpdateModel());
            logger.LogInformation("access have to db");
            if (updatedDetail == null)
            {
                logger.LogError("No detail found for product with ID {productId} for changing", productId);
                return NotFound($"No details found for product with ID {productId}.");
            }
            logger.LogInformation("product details are updated");


            return Ok(updatedDetail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while updating product details.");
            return new StatusCodeResult(500);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProductDetail(Guid productId)
    {
        try
        {
            var result = await productDetailService.DeleteProductDetailAsync(productId);
            if (!result)
            {
                logger.LogError("no details found for product with id {ProductId}", productId);
                return NotFound($"No details found for product with ID {productId}.");
            }
            logger.LogInformation("detail is deleted for id {ProductId}", productId);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while deleting product details.");
            return new StatusCodeResult(500);
        }
    }
}
