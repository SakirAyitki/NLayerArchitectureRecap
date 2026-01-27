using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

public class CarController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly ICarService _carService;

    public CarController(ICarService carService, IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var cars = await _carService.GetAllAsync();
        var carDtos = _mapper.Map<List<CarDto>>(cars);
        return CreateActionResult(CustomResponseDto<List<CarDto>>.Success(carDtos,200));
    }

    [ServiceFilter(typeof(NotFoundFilter<Car>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById(int id)
    {
        var car = await _carService.GetByIdAsync(id);
        var carDto = _mapper.Map<CarDto>(car);
        return CreateActionResult(CustomResponseDto<CarDto>.Success(carDto, 200));
    }

    [HttpPost]
    public async Task<IActionResult> AddCar(CarDto carDtos)
    {
        var car = await _carService.AddAsync(_mapper.Map<Car>(carDtos));
        var carDto = _mapper.Map<CarDto>(car);
        return CreateActionResult(CustomResponseDto<CarDto>.Success(carDto, 201));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CarUpdateDto carUpdateDto)
    {
        await _carService.UpdateAsync(_mapper.Map<Car>(carUpdateDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = await _carService.GetByIdAsync(id);
        await _carService.DeleteAsync(car);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCarsByBrand()
    {
        return CreateActionResult(await _carService.CarsByBrand());
    }
}