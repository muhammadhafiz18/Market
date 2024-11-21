using FluentValidation;
using Market.Dtos.ProductDetailDtos;

namespace Market.Validators;

public class ProductDetailUpdateDtoValidator : AbstractValidator<ProductDetailUpdateDto>
{
    public ProductDetailUpdateDtoValidator()
    {
        RuleFor(detail => detail.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(detail => detail.Description != null); // Validate only if provided.

        RuleFor(detail => detail.Color)
            .MaximumLength(50).WithMessage("Color cannot exceed 50 characters.")
            .When(detail => detail.Color != null);

        RuleFor(detail => detail.Material)
            .MaximumLength(100).WithMessage("Material cannot exceed 100 characters.")
            .When(detail => detail.Material != null);

        RuleFor(detail => detail.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than zero.");

        RuleFor(detail => detail.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock cannot be negative.");

        RuleFor(detail => detail.ManufactureDate)
            .LessThan(detail => detail.ExpiryDate).WithMessage("Manufacture date must be earlier than expiry date.");

        RuleFor(detail => detail.ExpiryDate)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Expiry date must be in the future.");

        RuleFor(detail => detail.Size)
            .MaximumLength(20).WithMessage("Size cannot exceed 20 characters.")
            .When(detail => detail.Size != null);

        RuleFor(detail => detail.Manufacturer)
            .MaximumLength(100).WithMessage("Manufacturer name cannot exceed 100 characters.")
            .When(detail => detail.Manufacturer != null);

        RuleFor(detail => detail.CountryOfOrigin)
            .MaximumLength(100).WithMessage("Country of origin cannot exceed 100 characters.")
            .When(detail => detail.CountryOfOrigin != null);
    }
}
