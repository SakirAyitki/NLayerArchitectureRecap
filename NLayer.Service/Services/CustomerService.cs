using AutoMapper;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class CustomerService : Service<Customer>, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    public CustomerService(IGenericRepository<Customer> repository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IMapper mapper) : base(repository, unitOfWork)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
}