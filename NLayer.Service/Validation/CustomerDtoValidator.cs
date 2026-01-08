using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validation;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(customer => customer.Email).NotNull().WithMessage("{PropertyName} is required").EmailAddress().WithMessage("{PropertyName} is required").MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");
        RuleFor(customer => customer.FirstName).NotNull().WithMessage("{PropertyName} is required").MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
        RuleFor(customer => customer.LastName).NotNull().WithMessage("{PropertyName} is required").MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
        RuleFor(customer => customer.Phone).NotNull().WithMessage("{PropertyName} is required").MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");
    }
}