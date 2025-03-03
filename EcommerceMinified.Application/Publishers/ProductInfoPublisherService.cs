using System;
using EcommerceMinified.Domain.Interfaces.Commands;
using EcommerceMinified.Domain.Interfaces.Publishers;
using EcommerceMinified.MsgContracts.Command;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EcommerceMinified.Application.Publishers;

public class ProductInfoPublisherService(ILogger<ProductInfoPublisherService> _logger, IPublishEndpoint _publishEndpoint) : IProductInfoPublisherService
{
    public async Task PublishProductInfo(IProductInfoCommand productInfoCommand)
    {
        try
        {
            await _publishEndpoint.Publish(productInfoCommand);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while publishing product info");
        }
    }
}
