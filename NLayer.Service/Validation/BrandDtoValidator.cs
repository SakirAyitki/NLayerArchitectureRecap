using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validation;

public class BrandDtoValidator : AbstractValidator<BrandDto>
{
    public BrandDtoValidator()
    {
        RuleFor(brand => brand.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required").MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
    }
}