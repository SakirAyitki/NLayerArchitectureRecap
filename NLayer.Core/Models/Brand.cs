namespace NLayer.Core.Models;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Car> Cars { get; set; }
}