using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds;

public class BrandSeed : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasData(
            new Brand {Id = 1, Name = "Ford", CreatedDate = new DateTime(2026, 1, 1)},
            new Brand {Id = 2, Name = "Opel", CreatedDate = new DateTime(2026, 1, 14)},
            new Brand {Id = 3, Name = "Nissan", CreatedDate = new DateTime(2026, 2, 22)},
            new Brand {Id = 4, Name = "Ferrari", CreatedDate = new DateTime(2026, 4, 11)},
            new Brand {Id = 5, Name = "Porsche", CreatedDate = new DateTime(2026, 5, 10)}
            );
    }
}