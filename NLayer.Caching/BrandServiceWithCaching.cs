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

public class BrandServiceWithCaching : IBrandService
{
    
    private const string BrandCacheKey = "brandCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IBrandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public BrandServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IBrandRepository repository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _repository = repository;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(BrandCacheKey, out _))
        {
            _memoryCache.Set(BrandCacheKey, _repository.GetAll());
        }

    }

    public async Task<Brand> AddAsync(Brand entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllBrandsAsync();
        return entity;
    }

    public async Task<IEnumerable<Brand>> AddRangeAsync(IEnumerable<Brand> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllBrandsAsync();
        return entities;
    }

    public async Task UpdateAsync(Brand entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllBrandsAsync();
    }

    public async Task DeleteAsync(Brand entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllBrandsAsync();
    }

    public async Task DeleteRange(IEnumerable<Brand> entities)
    {
        _repository.DeleteRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllBrandsAsync();
    }

    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await Task.FromResult(_memoryCache.Get<List<Brand>>(BrandCacheKey));
    }

    public IQueryable<Brand> Where(Expression<Func<Brand, bool>> expression)
    {
        return _memoryCache.Get<List<Brand>>(BrandCacheKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task<bool> AnyAsync(Expression<Func<Brand, bool>> expression)
    {
        return await Task.FromResult(_memoryCache.Get<List<Brand>>(BrandCacheKey).Any(expression.Compile())); 
    }

    public async Task<Brand> GetByIdAsync(int id)
    {
        var brand = _memoryCache.Get<List<Brand>>(BrandCacheKey).FirstOrDefault(x => x.Id == id);
        if (brand == null)
        {
            throw new NotFoundException($"Brand with id {id} not found");
        }
        return brand;
    }

    private async Task CacheAllBrandsAsync()
    {
        _memoryCache.Set(BrandCacheKey, await _repository.GetAll().ToListAsync());
    }
    
}