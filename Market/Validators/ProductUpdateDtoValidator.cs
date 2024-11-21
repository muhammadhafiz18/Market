using FluentValidation;
using Market.Dtos.ProductDtos;

namespace Market.Validators;
public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("Product name cannot be empty if provided.")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.")
            .When(product => product.Name != null); // Validate only if Name is provided.

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(product => product.Status)
            .IsInEnum().WithMessage("Invalid product status.");
    }
}

