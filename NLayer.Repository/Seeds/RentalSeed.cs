using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds;

public class RentalSeed : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasData(
            new Rental {Id = 1, CarId = 3, CreatedDate = new DateTime(2025, 1, 2), CustomerId = 2, RentDate =  new DateTime(2025, 1, 2), ReturnDate = new DateTime(2025,1,5), TotalPrice = 5500},
            new Rental {Id = 2, CarId = 1, CreatedDate = new DateTime(2025, 2, 23), CustomerId = 2, RentDate =  new DateTime(2025, 2, 23), ReturnDate = new DateTime(2025,2,30), TotalPrice = 10500},
            new Rental {Id = 3, CarId = 2, CreatedDate = new DateTime(2025, 12, 5), CustomerId = 2, RentDate =  new DateTime(2025, 12, 5), ReturnDate = new DateTime(2025,12,6), TotalPrice = 9500},
            new Rental {Id = 4, CarId = 5, CreatedDate = new DateTime(2026, 1, 2), CustomerId = 2, RentDate =  new DateTime(2026, 1, 2), ReturnDate = new DateTime(2026,1,4), TotalPrice = 125000},
            new Rental {Id = 5, CarId = 4, CreatedDate = new DateTime(2026, 3, 4), CustomerId = 2, RentDate =  new DateTime(2026, 3, 4), ReturnDate = new DateTime(2026,3,5), TotalPrice = 55000}
        );
    }
}