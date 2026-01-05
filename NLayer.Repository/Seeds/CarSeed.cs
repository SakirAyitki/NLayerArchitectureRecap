using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds;

public class CarSeed : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasData(
            new Car {Id = 1, CreatedDate = new DateTime(2026, 1, 1),BrandId = 1, DailyPrice = 6500, isAvailable =  true, Mileage = 120000, Model = "Ranger", Plate = "34 AHC 16", Year = 2025},
            new Car {Id = 2, CreatedDate = new DateTime(2026, 2, 4),BrandId = 2, DailyPrice = 2400, isAvailable =  true, Mileage = 256000, Model = "Astra", Plate = "34 TF 21", Year = 2018},
            new Car {Id = 3, CreatedDate = new DateTime(2026, 4, 12),BrandId = 3, DailyPrice = 3200, isAvailable =  false, Mileage = 310000, Model = "Qashqai", Plate = "52 ORD 39", Year = 2022},
            new Car {Id = 4, CreatedDate = new DateTime(2025, 11, 15),BrandId = 4, DailyPrice = 65000, isAvailable =  true, Mileage = 10000, Model = "La Ferrari", Plate = "34 GEL 34", Year = 2023},
            new Car {Id = 5, CreatedDate = new DateTime(2025, 12, 14),BrandId = 5, DailyPrice = 45000, isAvailable =  true, Mileage = 13540, Model = "GT3 Turbo", Plate = "06 BNM 112", Year = 2022}
        );
    }
}