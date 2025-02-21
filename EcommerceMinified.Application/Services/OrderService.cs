using System;
using AutoMapper;
using EcommerceMinified.Domain.Entity;
using EcommerceMinified.Domain.Enum;
using EcommerceMinified.Domain.Exceptions;
using EcommerceMinified.Domain.Interfaces.Repository;
using EcommerceMinified.Domain.Interfaces.Services;
using EcommerceMinified.Domain.ViewModel.DTOs;

namespace EcommerceMinified.Application.Services;

public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper) : IOrderService
{
    public async Task<OrderDto> CreateOrderAsync(OrderDto order)
    {
        if(order.Items.Count == 0)
        {
            throw new EcommerceMinifiedDomainException("Order must have at least one item", ErrorCodeEnum.BadRequest);
        }

        var newOrder = new Order
        {
            CustomerId = order.CustomerId,
            Total = order.Total,
            Status = order.Status,
            OrderDate = order.OrderDate,
            Items = order.Items.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price
            }).ToList()
        };

        _unitOfWork.OrderRepository.Insert(newOrder);
        await _unitOfWork.CommitPostresAsync();

        return _mapper.Map<OrderDto>(newOrder);
    }

    public Task DeleteOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderDto>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> UpdateOrderAsync(OrderDto order)
    {
        throw new NotImplementedException();
    }
}
