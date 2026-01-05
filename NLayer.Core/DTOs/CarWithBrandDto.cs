using NLayer.Core.Models;

namespace NLayer.Core.DTOs;

public class CarWithBrandDto : CarDto
{
    public BrandDto Brand { get; set; }
}