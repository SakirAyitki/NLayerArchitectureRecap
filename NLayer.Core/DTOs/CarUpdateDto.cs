namespace NLayer.Core.DTOs;

public class CarUpdateDto
{
    public int Year { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public decimal DailyPrice { get; set; }
    public int Mileage { get; set; }
    public Boolean isAvailable { get; set; }
    public int BrandId { get; set; }
}