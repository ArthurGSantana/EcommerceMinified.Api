using System;

namespace EcommerceMinified.Domain.Entity;

public class Product : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int Category { get; set; }
    public string? Image { get; set; } = string.Empty;
}
