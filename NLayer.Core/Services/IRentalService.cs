using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services;

public interface IRentalService : IService<Rental>
{
    Task<CustomResponseDto<List<RentalWithCustomerDto>>> RentalsWithCustomer();
}