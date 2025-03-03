using System;
using EcommerceMinified.MsgContracts.Command;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EcommerceMinified.Application.Publishers;

public class ProductInfoPublisherService(ILogger<ProductInfoPublisherService> _logger, IPublishEndpoint _publishEndpoint)
{
    public async Task PublishProductInfo(ProductInfoCommand productInfoCommand)
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
