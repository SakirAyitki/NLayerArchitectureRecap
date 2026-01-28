using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;

namespace NLayer.Caching;

public class CustomerServiceWithCaching : ICustomerService
{
    private const string BrandCacheKey = "brandCache";
    private readonly ICustomerRepository _repository;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerServiceWithCaching(ICustomerRepository repository, IMemoryCache memoryCache, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _memoryCache = memoryCache;
        _mapper = mapper;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(BrandCacheKey, out _))
        {
            _memoryCache.Set(BrandCacheKey, _repository.GetAll().ToList());
        }
    }

    public async Task<Customer> AddAsync(Customer entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllCustomersAsync();
        return entity;
    }

    public async Task<IEnumerable<Customer>> AddRangeAsync(IEnumerable<Customer> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllCustomersAsync();
        return entities;
    }

    public async Task UpdateAsync(Customer entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllCustomersAsync();
    }

    public async Task DeleteAsync(Customer entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllCustomersAsync();
    }

    public async Task DeleteRange(IEnumerable<Customer> entities)
    {
        _repository.DeleteRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllCustomersAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await Task.FromResult(_memoryCache.Get<IEnumerable<Customer>>(BrandCacheKey));
    }

    public IQueryable<Customer> Where(Expression<Func<Customer, bool>> expression)
    {
        return _memoryCache.Get<List<Customer>>(BrandCacheKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task<bool> AnyAsync(Expression<Func<Customer, bool>> expression)
    {
        return await Task.FromResult(_memoryCache.Get<List<Customer>>(BrandCacheKey).Any(expression.Compile()));
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var customer = await Task.FromResult(_memoryCache.Get<List<Customer>>(BrandCacheKey).FirstOrDefault(x => x.Id == id));
        if (customer != null)
        {
            throw new NotFoundException($"Customer with id {id} not found");
        }

        return customer;
    }
    
    
    private async Task CacheAllCustomersAsync()
    {
        _memoryCache.Set(BrandCacheKey, await _repository.GetAll().ToListAsync());
    }
    
}