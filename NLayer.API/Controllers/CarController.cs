using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

public class CarController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly ICarService _service;

    public CarController(ICarService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var cars = _service.GetAll();
        var carDtos = _mapper.Map<List<CarDto>>(cars);
        return CreateActionResult(CustomResponseDto<List<CarDto>>.Success(carDtos,200));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById(int id)
    {
        var car = await _service.GetByIdAsync(id);
        var carDto = _mapper.Map<CarDto>(car);
        return CreateActionResult(CustomResponseDto<CarDto>.Success(carDto, 200));
    }

    [HttpPost]
    public async Task<IActionResult> AddCar(CarDto carDtos)
    {
        var car = await _service.AddAsync(_mapper.Map<Car>(carDtos));
        var carDto = _mapper.Map<CarDto>(car);
        return CreateActionResult(CustomResponseDto<CarDto>.Success(carDto, 201));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(CarUpdateDto carUpdateDto)
    {
        await _service.Update(_mapper.Map<Car>(carUpdateDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var car = await _service.GetByIdAsync(id);
        await _service.Delete(car);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCarsByBrand()
    {
        return CreateActionResult(await _service.CarsByBrand());
    }
}