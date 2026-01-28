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

public class RentalServiceWithCaching : IRentalService
{
    
    private const string CacheRentalKey = "rentalCache";
    private readonly IRentalRepository _repository;
    private readonly IMemoryCache _memoryCache;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RentalServiceWithCaching(IRentalRepository repository, IMemoryCache memoryCache, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _memoryCache = memoryCache;
        _unitOfWork = unitOfWork;
        _mapper = mapper;

        if (!_memoryCache.TryGetValue(CacheRentalKey, out _))
        {
            _memoryCache.Set(CacheRentalKey, _repository.RentalsWithCustomer().Result);
        }
    }

    public async Task<Rental> AddAsync(Rental entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllRentalsAsync();
        return entity;
    }

    public async Task<IEnumerable<Rental>> AddRangeAsync(IEnumerable<Rental> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllRentalsAsync();
        return entities;
    }

    public async Task UpdateAsync(Rental entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllRentalsAsync();
    }

    public async Task DeleteAsync(Rental entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllRentalsAsync();
    }

    public async Task DeleteRange(IEnumerable<Rental> entities)
    {
        _repository.DeleteRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllRentalsAsync();
    }

    public async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await Task.FromResult(_memoryCache.Get<List<Rental>>(CacheRentalKey));
    }

    public IQueryable<Rental> Where(Expression<Func<Rental, bool>> expression)
    {
        return _memoryCache.Get<IEnumerable<Rental>>(CacheRentalKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task<bool> AnyAsync(Expression<Func<Rental, bool>> expression)
    {
        return await Task.FromResult(_memoryCache.Get<List<Rental>>(CacheRentalKey).Any(expression.Compile()));
    }

    public async Task<Rental> GetByIdAsync(int id)
    {
        var rental = await Task.FromResult(_memoryCache.Get<List<Rental>>(CacheRentalKey).FirstOrDefault(x => x.Id == id));
        if (rental == null)
        {
            throw new NotFoundException($"Rental with id {id} not found");
        }
        return rental;
    }

    public async Task<CustomResponseDto<List<RentalWithCustomerDto>>> RentalsWithCustomer()
    {
        var rentalWithCustomer = await Task.FromResult(_memoryCache.Get<List<Rental>>(CacheRentalKey));
        var rentalWithCustomerDto = _mapper.Map<List<RentalWithCustomerDto>>(rentalWithCustomer);
        return CustomResponseDto<List<RentalWithCustomerDto>>.Success(rentalWithCustomerDto, 200);
    }

    private async Task CacheAllRentalsAsync()
    {
        await _memoryCache.Set(CacheRentalKey, _repository.GetAll().ToListAsync());
    } 
    
}