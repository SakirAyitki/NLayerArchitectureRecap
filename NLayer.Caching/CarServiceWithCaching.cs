using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;

namespace NLayer.Caching;

public class CarServiceWithCaching : ICarService
{
    
    private const string CarCacheKey = "carCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly ICarRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CarServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, ICarRepository repository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _repository = repository;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(CarCacheKey, out _))
        {
            _memoryCache.Set(CarCacheKey, _repository.CarsByBrand().Result);
        }

    }

    public async Task<Car> AddAsync(Car entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllCars();
        return entity;
    }

    public async Task<IEnumerable<Car>> AddRangeAsync(IEnumerable<Car> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllCars();
        return entities;
    }

    public async Task UpdateAsync(Car entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllCars();
    }

    public async Task DeleteAsync(Car entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllCars();
    }

    public async Task DeleteRange(IEnumerable<Car> entities)
    {
        _repository.DeleteRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllCars();
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        return await Task.FromResult(_memoryCache.Get<IEnumerable<Car>>(CarCacheKey));
    }

    public IQueryable<Car> Where(Expression<Func<Car, bool>> expression)
    {
        return _memoryCache.Get<List<Car>>(CarCacheKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task<bool> AnyAsync(Expression<Func<Car, bool>> expression)
    {
       return await _repository.AnyAsync(expression);
    }

    public async Task<Car> GetByIdAsync(int id)
    {
        var car = await Task.FromResult(_memoryCache.Get<List<Car>>(CarCacheKey).FirstOrDefault(x=>x.Id==id));
        if (car == null)
        {
            throw new NotFoundException($"Car with id {id} not found");
        }
        return car;
    }

    public async Task<CustomResponseDto<List<CarWithBrandDto>>> CarsByBrand()
    {
        var carsByBrand = await Task.FromResult(_memoryCache.Get<List<Car>>(CarCacheKey));
        var carsByBrandDto = _mapper.Map<List<CarWithBrandDto>>(carsByBrand);
        return CustomResponseDto<List<CarWithBrandDto>>.Success(carsByBrandDto,200);
    }

    public async Task CacheAllCars()
    {
        _memoryCache.Set(CarCacheKey, await _repository.GetAll().ToListAsync());
    }
    
}