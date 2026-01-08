using System.Data;
using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validation;

public class CarDtoValidator : AbstractValidator<CarDto>
{
    public CarDtoValidator()
    {
        RuleFor(car=>car.DailyPrice).NotNull().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(car => car.isAvailable).NotNull().WithMessage("{PropertyName} is required");
        RuleFor(car=>car.Mileage).NotNull().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(car => car.Model).NotNull().WithMessage("{PropertyName} is required").MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
        RuleFor(car => car.Year).NotNull().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(car=>car.Plate).NotNull().WithMessage("{PropertyName} is required").MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");
    }
}