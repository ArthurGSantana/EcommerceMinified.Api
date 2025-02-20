using System;

namespace EcommerceMinified.Domain.ViewModel.DTOs;

public class BaseDto
{
    public Guid? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
}
