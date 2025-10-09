using Microsoft.Extensions.Caching.Distributed;
using OrderMgmt.API.Infrastructure.MessageBroker;

namespace OrderMgmt.API.Infrastructure.BackgroundServices;

public class ProductUpdateMesageHostedService : BackgroundService
{
    private readonly IRabbitMqConsumer _rabbitMqConsumer;
    private readonly ILogger<ProductUpdateMesageHostedService> _logger;
    private readonly IDistributedCache _cache;
    private readonly IMapper _mapper;

    public ProductUpdateMesageHostedService(
        IRabbitMqConsumer rabbitMqConsumer,
        IDistributedCache cache,
        ILogger<ProductUpdateMesageHostedService> logger,
        IMapper mapper)
    {
        _rabbitMqConsumer = rabbitMqConsumer;
        _cache = cache;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _rabbitMqConsumer.StartConsumingAsync(
                queueName: "product_update_queue",
                routingKey: "product.update",
                onMessageReceived: async (UpdateProductMessage message) =>
                {
                    _logger.LogInformation("Received product update message: {Message}", message);
                    await ProcessProductMessageAsync(message);
                });

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in RabbitMQ Background Service");
        }
    }

    // this service invalidates orders cache when product is updated
    private async Task ProcessProductMessageAsync(UpdateProductMessage message)
    {
        var cacheKey = $"product_{message.ProductID}";
        var product = _cache.GetString(cacheKey);

        if (product is null)
        {
            return;
        }

        var productResponse = _mapper.Map<ProductResponse>(message);
        var productJson = JsonSerializer.Serialize(productResponse);

        await _cache.SetStringAsync(cacheKey, productJson);

        _logger.LogInformation("{HostedService}: Cached update product with id {ProductID} successfully.",nameof(ProductUpdateMesageHostedService), message.ProductID);

        await Task.CompletedTask;
    }
}
