using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Brand, BrandDto>().ReverseMap();
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Rental, RentalDto>().ReverseMap();
        CreateMap<CarUpdateDto, Car>();
        CreateMap<Car, CarWithBrandDto>();
    }
}