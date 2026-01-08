using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validation;

public class RentalDtoValidator : AbstractValidator<RentalDto>
{
    public RentalDtoValidator()
    {
        RuleFor(rental => rental.CarId).NotNull().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(rental => rental.CustomerId).NotNull().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(rental => rental.RentDate).NotNull().WithMessage("{PropertyName} is required");
        RuleFor(rental => rental.ReturnDate).NotNull().WithMessage("{PropertyName} is required");
        RuleFor(rental => rental.TotalPrice).NotNull().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}