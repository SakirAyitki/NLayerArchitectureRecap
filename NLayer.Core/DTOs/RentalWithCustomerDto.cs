namespace NLayer.Core.DTOs;

public class RentalWithCustomerDto : RentalDto
{
    public CustomerDto Customer { get; set; }
}