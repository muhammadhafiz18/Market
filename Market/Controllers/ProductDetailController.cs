using Microsoft.AspNetCore.Mvc;
using Market.Interfaces;
using Market.Mappers;

namespace Market.Controllers;

[ApiController]
[Route("api/products/{productId}/details")]
public class ProductDetailsController(IProductDetailService productDetailService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProductDetail(Guid productId, [FromBody] Dtos.ProductDetailDtos.ProductDetailCreateDto detail)
    {
        if (detail == null)
        {
            return BadRequest("Product detail cannot be null.");
        }


        var createdDetail = await productDetailService.CreateProductDetailAsync(productId, detail.ToModel());
        return CreatedAtAction(nameof(GetProductDetails), new { productId }, createdDetail);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductDetails(Guid productId)
    {
        var detail = await productDetailService.GetDetailsByProductIdAsync(productId);
        if (detail == null)
        {
            return NotFound($"No details found for product with ID {productId}.");
        }
        return Ok(detail);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProductDetail(Guid productId, [FromBody] Dtos.ProductDetailDtos.ProductDetailUpdateDto detail)
    {
        if (detail == null)
        {
            return BadRequest("Product detail cannot be null.");
        }

        var updatedDetail = await productDetailService.UpdateProductDetailAsync(productId, detail.ToUpdateModel());
        if (updatedDetail == null)
        {
            return NotFound($"No details found for product with ID {productId}.");
        }
        return Ok(updatedDetail);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProductDetail(Guid productId)
    {
        var result = await productDetailService.DeleteProductDetailAsync(productId);
        if (!result)
        {
            return NotFound($"No details found for product with ID {productId}.");
        }
        return NoContent();
    }
}
