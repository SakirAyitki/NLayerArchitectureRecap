namespace NLayer.Core.DTOs;

public class RentalDto
{
    public int CarId { get; set; }
    
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
    
    public int CustomerId { get; set; }
    
    public decimal TotalPrice { get; set; }
}