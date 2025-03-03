using System;

namespace EcommerceMinified.MsgContracts.Command;

public class ProductInfoCommand
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductWeight { get; set; }
}
