namespace NLayer.Core.Models;

public class Car : BaseEntity
{
    public int Year { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public decimal DailyPrice { get; set; }
    public int Mileage { get; set; }
    public Boolean isAvailable { get; set; }
    public Brand Brand { get; set; }
    public int BrandId { get; set; }
    public ICollection<Rental> RentalHistory { get; set; }
}