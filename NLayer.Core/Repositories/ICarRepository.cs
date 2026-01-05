using NLayer.Core.Models;

namespace NLayer.Core.Repositories;

public interface ICarRepository : IGenericRepository<Car>
{
    Task<List<Car>> CarsByBrand();
}