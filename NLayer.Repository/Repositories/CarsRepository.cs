using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories;

public class CarsRepository : GenericRepository<Car>, ICarRepository
{
    public CarsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Car>> CarsByBrand()
    {
        return await _context.Cars.Include(x=>x.Brand).ToListAsync();
    }
}