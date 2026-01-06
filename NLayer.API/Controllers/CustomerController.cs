using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers;

public class CustomerController : CustomBaseController
{
    private readonly IMapper _mapper;
    private readonly ICustomerService _customerService;
    
    public CustomerController(IMapper mapper, ICustomerService customerService)
    {
        _mapper = mapper;
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
        return CreateActionResult(CustomResponseDto<List<CustomerDto>>.Success(customerDtos,200));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        var customerDto = _mapper.Map<CustomerDto>(customer);
        return CreateActionResult(CustomResponseDto<CustomerDto>.Success(customerDto, 200));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CustomerUpdateDto customerUpdateDto)
    {
        await _customerService.UpdateAsync(_mapper.Map<Customer>(customerUpdateDto));
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }

    [HttpPost]
    public async Task<IActionResult> Add(CustomerDto customerDto)
    {
        var customer = await _customerService.AddAsync(_mapper.Map<Customer>(customerDto));
        var customerDtos = _mapper.Map<CustomerDto>(customer);
        return CreateActionResult(CustomResponseDto<CustomerDto>.Success(customerDtos,204));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        await _customerService.DeleteAsync(customer);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}