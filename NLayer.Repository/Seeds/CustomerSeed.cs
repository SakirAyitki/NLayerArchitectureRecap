using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds;

public class CustomerSeed : IEntityTypeConfiguration<CustomerSeed>
{
    public void Configure(EntityTypeBuilder<CustomerSeed> builder)
    {
        builder.HasData(
            new Customer() {Id = 1, FirstName = "Şakir", LastName = "Ayitki", Phone = "+90 555 555 55 55", Email = "ayitkisakir@gmail.com", CreatedDate = new DateTime(2026, 1, 1)},
            new Customer() {Id = 2, FirstName = "Elif", LastName = "Ayitki", Phone = "+90 444 444 44 44", Email = "eiayitki@gmail.com", CreatedDate = new DateTime(2026, 3, 5)},
            new Customer() {Id = 3, FirstName = "Ferhat", LastName = "Cakmak", Phone = "+90 333 333 33 33", Email = "ferhatcakmak@gmail.com", CreatedDate = new DateTime(2026, 5, 12)},
            new Customer() {Id = 4, FirstName = "Muhammet", LastName = "Kara", Phone = "+90 777 777 77 77", Email = "muhammet@gmail.com", CreatedDate = new DateTime(2026, 6, 17)}
        );
    }
}