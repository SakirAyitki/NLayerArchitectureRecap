using AutoMapper;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class BrandService : Service<Brand>, IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    
    public BrandService(IGenericRepository<Brand> repository, IUnitOfWork unitOfWork, IBrandRepository brandRepository, IMapper mapper) : base(repository, unitOfWork)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }
}