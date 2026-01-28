using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class CarServiceWithNoCaching : Service<Car>, ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;
    
    public CarServiceWithNoCaching(IGenericRepository<Car> repository, IUnitOfWork unitOfWork, ICarRepository carRepository, IMapper mapper) : base(repository, unitOfWork)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<List<CarWithBrandDto>>> CarsByBrand()
    {
        var cars = await _carRepository.CarsByBrand();
        var carsDto = _mapper.Map<List<CarWithBrandDto>>(cars);
        return CustomResponseDto<List<CarWithBrandDto>>.Success(carsDto, 200);
    }
}