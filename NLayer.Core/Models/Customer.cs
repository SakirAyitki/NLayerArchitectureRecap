namespace NLayer.Core.Models;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ICollection<Rental>? Rentals { get; set; }
}