using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x=> x.DailyPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x=>x.Year).IsRequired().HasColumnType("int");
        builder.Property(x=>x.isAvailable).IsRequired().HasColumnType("bit");
        builder.Property(x=>x.Mileage).IsRequired().HasColumnType("int");
        builder.Property(x=>x.Plate).IsRequired().HasColumnType("nvarchar(20)");
        
        builder.HasOne(x=>x.Brand).WithMany(x=>x.Cars).HasForeignKey(x => x.BrandId);
        builder.HasMany(x=>x.RentalHistory).WithOne(x=>x.Car).HasForeignKey(x => x.CarId);
    }
}