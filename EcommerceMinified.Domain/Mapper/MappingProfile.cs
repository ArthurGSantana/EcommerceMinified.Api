using System;
using AutoMapper;
using EcommerceMinified.Domain.Entity;
using EcommerceMinified.Domain.ViewModel.DTOs;

namespace EcommerceMinified.Domain.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
