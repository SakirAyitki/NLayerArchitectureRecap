using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

public class RentalController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly IRentalService _rentalService;

    public RentalController(IMapper mapper, IRentalService rentalService)
    {
        _mapper = mapper;
        _rentalService = rentalService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rentals = await _rentalService.GetAllAsync();
        var rentalDtos = _mapper.Map<List<RentalDto>>(rentals);
        return CreateActionResult(CustomResponseDto<List<RentalDto>>.Success(rentalDtos,200));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var rental = await _rentalService.GetByIdAsync(id);
        var rentalDtos = _mapper.Map<RentalDto>(rental);
        return CreateActionResult(CustomResponseDto<RentalDto>.Success(rentalDtos,200));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> RentalsWithCustomers()
    {
        return CreateActionResult(await _rentalService.RentalsWithCustomer());
    }
}