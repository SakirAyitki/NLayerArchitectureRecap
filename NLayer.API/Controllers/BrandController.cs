using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

public class BrandController : CustomBaseController
{
    private readonly IBrandService _brandService;
    private readonly IMapper _mapper;

    public BrandController(IBrandService brandService, IMapper mapper)
    {
        _brandService = brandService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _brandService.GetAllAsync();
        var brandDto = _mapper.Map<List<BrandDto>>(brands);
        return CreateActionResult(CustomResponseDto<List<BrandDto>>.Success(brandDto,200));
    }
    
    [ServiceFilter(typeof(NotFoundFilter<Brand>))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var brand = await _brandService.GetByIdAsync(id);
        var brandDto = _mapper.Map<BrandDto>(brand);
        return CreateActionResult(CustomResponseDto<BrandDto>.Success(brandDto,200));
    }

    [HttpPut]
    public async Task<IActionResult> Update(BrandUpdateDto brandUpdateDto)
    {
        await _brandService.UpdateAsync(_mapper.Map<Brand>(brandUpdateDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpPost]
    public async Task<IActionResult> Add(BrandDto brandDto)
    {
        var brand = await _brandService.AddAsync(_mapper.Map<Brand>(brandDto));
        var brandDtos = _mapper.Map<BrandDto>(brand);
        return CreateActionResult(CustomResponseDto<BrandDto>.Success(brandDtos,201));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _brandService.GetByIdAsync(id);
        await _brandService.DeleteAsync(brand);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}