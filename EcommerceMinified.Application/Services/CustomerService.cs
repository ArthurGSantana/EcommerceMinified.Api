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

    public async Task DeleteCustomerAsync(Guid id)
    {
        var exists = await _unitOfWork.CustomerRepository.GetAsync(false, null, x => x.Id == id);

        if (exists == null)
        {
            throw new EcommerceMinifiedDomainException("Customer not found", ErrorCodeEnum.NotFound);
        }

        _unitOfWork.CustomerRepository.Delete(exists);
        await _unitOfWork.CommitPostresAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _unitOfWork.CustomerRepository.GetAsync(false, null, x => x.Id == id);

        if (customer == null)
        {
            throw new EcommerceMinifiedDomainException("Customer not found", ErrorCodeEnum.NotFound);
        }

        return customer;
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await _unitOfWork.CustomerRepository.GetAllAsync();
    }

    public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customer)
    {
        var exists = await _unitOfWork.CustomerRepository.GetAsync(true, null, x => x.Id == customer.Id);

        if (exists == null)
        {
            throw new EcommerceMinifiedDomainException("Customer not found", ErrorCodeEnum.NotFound);
        }

        var updatedCustomer = _mapper.Map<Customer>(customer);

        _unitOfWork.CustomerRepository.Update(updatedCustomer);
        await _unitOfWork.CommitPostresAsync();

        return _mapper.Map<CustomerDto>(updatedCustomer);
    }
}
