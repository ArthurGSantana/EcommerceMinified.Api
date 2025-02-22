using System;
using EcommerceMinified.Domain.ViewModel.DTOs;
using FluentValidation;

namespace EcommerceMinified.Domain.Validators;

public class OrderItemValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order is required");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product is required");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity is required")
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}
