using System;

namespace EcommerceMinified.Domain.Entity;

public class Order : Base
{
    public Guid CustomerId { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? OrderDate { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    
}
