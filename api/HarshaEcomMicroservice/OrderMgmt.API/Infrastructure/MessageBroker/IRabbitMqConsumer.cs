
namespace OrderMgmt.API.Infrastructure.MessageBroker;

public interface IRabbitMqConsumer
{
    Task StartConsumingAsync<T>(string queueName, string routingKey, Func<T, Task> onMessageReceived);
    void Dispose();
}