using System;
using AutoMapper;
using EcommerceMinified.Domain.Entity;
using EcommerceMinified.Domain.Enum;
using EcommerceMinified.Domain.Exceptions;
using EcommerceMinified.Domain.Interfaces.Repository;
using EcommerceMinified.Domain.Interfaces.Services;
using EcommerceMinified.Domain.ViewModel.DTOs;

namespace EcommerceMinified.Application.Services;

public class CustomerService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICustomerService
{
    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customer)
    {
        var exists = await _unitOfWork.CustomerRepository.GetAsync(false, null, x => x.Email == customer.Email);

        if (exists != null)
        {
            throw new EcommerceMinifiedDomainException("Customer already exists", ErrorCodeEnum.AlreadyExists);
        }

        var newCustomer = _mapper.Map<Customer>(customer);

        _unitOfWork.CustomerRepository.Insert(newCustomer);
        await _unitOfWork.CommitPostresAsync();

        return _mapper.Map<CustomerDto>(newCustomer);
    }

    public Task DeleteCustomerAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetCustomerByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Customer>> GetCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CustomerDto> UpdateCustomerAsync(CustomerDto customer)
    {
        throw new NotImplementedException();
    }
}
