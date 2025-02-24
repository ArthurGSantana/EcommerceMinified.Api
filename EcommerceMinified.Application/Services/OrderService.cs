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
        if (order.Items == null || order?.Items?.Count == 0)
        {
            throw new EcommerceMinifiedDomainException("Order must have at least one item", ErrorCodeEnum.BadRequest);
        }

        var newOrder = new Order
        {
            CustomerId = order.CustomerId,
            Total = order.Items.Sum(x => x.Price * x.Quantity),
            Status = OrderStatusEnum.Pending,
            OrderDate = DateTime.Now,
            Items = order.Items.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price
            }).ToList()
        };

        _unitOfWork.OrderRepository.Insert(newOrder);
        await _unitOfWork.CommitPostgresAsync();

        return _mapper.Map<OrderDto>(newOrder);
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetAsync(false, null, x => x.Id == id);

        if (order == null)
        {
            throw new EcommerceMinifiedDomainException("Order not found", ErrorCodeEnum.NotFound);
        }

        _unitOfWork.OrderRepository.Delete(order);
        await _unitOfWork.CommitPostgresAsync();
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetAsync(false, null, x => x.Id == id);

        if (order == null)
        {
            throw new EcommerceMinifiedDomainException("Order not found", ErrorCodeEnum.NotFound);
        }

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var orders = await _unitOfWork.OrderRepository.GetAllAsync();
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto> UpdateOrderAsync(OrderDto order)
    {
        var currentOrder = await _unitOfWork.OrderRepository.GetAsync(true, null, x => x.Id == order.Id);

        if (currentOrder == null)
        {
            throw new EcommerceMinifiedDomainException("Order not found", ErrorCodeEnum.NotFound);
        }

        currentOrder.Status = order.Status;

        _unitOfWork.OrderRepository.Update(currentOrder);
        await _unitOfWork.CommitPostgresAsync();

        return _mapper.Map<OrderDto>(currentOrder);
    }
}
