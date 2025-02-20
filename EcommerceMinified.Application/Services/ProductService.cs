using System;
using EcommerceMinified.Domain.Entity;
using EcommerceMinified.Domain.Interfaces.Services;

namespace EcommerceMinified.Application.Services;

public class ProductService : IProductService
{
    public Task<Product> CreateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
