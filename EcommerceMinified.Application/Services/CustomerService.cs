using System;
using EcommerceMinified.Domain.Entity;
using EcommerceMinified.Domain.Enum;
using EcommerceMinified.Domain.Exceptions;
using EcommerceMinified.Domain.Interfaces.Repository;
using EcommerceMinified.Domain.Interfaces.Services;
using EcommerceMinified.Domain.ViewModel.DTOs;

namespace EcommerceMinified.Application.Services;

public class CustomerService(IUnitOfWork _unitOfWork) : ICustomerService
{
    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customer)
    {
        var exists = await _unitOfWork.CustomerRepository.GetAsync(false, null, x => x.Email == customer.Email);

        if (exists != null)
        {
            throw new EcommerceMinifiedDomainException("Customer already exists", ErrorCodeEnum.AlreadyExists);
        }

        var newCustomer = new Customer
        {
            Name = customer.Name,
            Email = customer.Email,
            Password = customer.Password,
            Phone = customer.Phone,
            Image = customer.Image,
            Address = new Address
            {
                Street = customer.Address.Street,
                Number = customer.Address.Number,
                Complement = customer.Address.Complement,
                Neighborhood = customer.Address.Neighborhood,
                City = customer.Address.City,
                State = customer.Address.State,
                ZipCode = customer.Address.ZipCode
            }
        };

        _unitOfWork.CustomerRepository.Insert(newCustomer);
        await _unitOfWork.CommitPostresAsync();

        return 
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

    public Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}
