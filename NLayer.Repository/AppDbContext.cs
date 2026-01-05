using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;

namespace NLayer.Repository;

public class AppDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}