namespace NLayer.Core.Models;

public class Rental : BaseEntity
{
    public int CarId { get; set; }
    public Car Car { get; set; }
    
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public decimal TotalPrice { get; set; }
}