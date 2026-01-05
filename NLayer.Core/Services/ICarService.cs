using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services;

public interface ICarService : IService<Car>
{
    Task<CustomResponseDto<List<CarWithBrandDto>>> CarsByBrand();
}