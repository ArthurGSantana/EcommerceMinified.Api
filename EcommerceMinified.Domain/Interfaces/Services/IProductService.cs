using System;
using EcommerceMinified.Domain.Entity;

namespace EcommerceMinified.Domain.Interfaces.Services;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(Guid id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
    Task<List<Product>> GetProductsAsync();
}
