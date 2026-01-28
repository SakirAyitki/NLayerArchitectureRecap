using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class RentalServiceWithNoCaching : Service<Rental>, IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IMapper _mapper;
    
    public RentalServiceWithNoCaching(IGenericRepository<Rental> repository, IUnitOfWork unitOfWork, IRentalRepository rentalRepository, IMapper mapper) : base(repository, unitOfWork)
    {
        _rentalRepository = rentalRepository;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<List<RentalWithCustomerDto>>> RentalsWithCustomer()
    {
        var rentals = await _rentalRepository.RentalsWithCustomer();
        var rentalsDto = _mapper.Map<List<RentalWithCustomerDto>>(rentals);
        return CustomResponseDto<List<RentalWithCustomerDto>>.Success(rentalsDto,200);
    }
}