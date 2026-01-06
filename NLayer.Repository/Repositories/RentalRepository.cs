using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories;

public class RentalRepository : GenericRepository<Rental>, IRentalRepository
{
    public RentalRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Rental>> RentalsWithCustomer()
    {
        return await _context.Rentals.Include(x => x.Customer).ToListAsync();
    }
}