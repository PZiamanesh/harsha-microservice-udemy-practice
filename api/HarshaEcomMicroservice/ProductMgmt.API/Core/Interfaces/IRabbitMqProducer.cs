namespace ProductMgmt.API.Core.Interfaces;

public interface IRabbitMqProducer
{
    Task PublishAsync<T>(string routingKey, T message);
}
