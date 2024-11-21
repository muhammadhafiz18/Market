using FluentValidation;
using Market.Dtos.ProductDetailDtos;
using System;

namespace Market.Validators;
public class ProductDetailCreateDtoValidator : AbstractValidator<ProductDetailCreateDto>
{
    public ProductDetailCreateDtoValidator()
    {
        RuleFor(productDetail => productDetail.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(productDetail => productDetail.Color)
            .NotEmpty().WithMessage("Color is required.")
            .MaximumLength(50).WithMessage("Color cannot exceed 50 characters.");

        RuleFor(productDetail => productDetail.Material)
            .NotEmpty().WithMessage("Material is required.")
            .MaximumLength(100).WithMessage("Material cannot exceed 100 characters.");

        RuleFor(productDetail => productDetail.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than zero.");

        RuleFor(productDetail => productDetail.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock cannot be negative.");

        RuleFor(productDetail => productDetail.ManufactureDate)
            .LessThan(productDetail => productDetail.ExpiryDate).WithMessage("Manufacture date must be earlier than expiry date.");

        RuleFor(productDetail => productDetail.ExpiryDate)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Expiry date must be in the future.");

        RuleFor(productDetail => productDetail.Size)
            .NotEmpty().WithMessage("Size is required.")
            .MaximumLength(20).WithMessage("Size cannot exceed 20 characters.");

        RuleFor(productDetail => productDetail.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required.")
            .MaximumLength(100).WithMessage("Manufacturer name cannot exceed 100 characters.");

        RuleFor(productDetail => productDetail.CountryOfOrigin)
            .NotEmpty().WithMessage("Country of origin is required.")
            .MaximumLength(100).WithMessage("Country of origin cannot exceed 100 characters.");
    }
}
