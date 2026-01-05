using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x=>x.RentDate).IsRequired().HasColumnType("date");
        builder.Property(x=>x.ReturnDate).IsRequired().HasColumnType("date");
        builder.Property(x=>x.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
        
        
    }
}