using NLayer.Core.Models;

namespace NLayer.Core.Repositories;

public interface IRentalRepository : IGenericRepository<Rental>
{
    Task<List<Rental>> RentalsWithCustomer();
}