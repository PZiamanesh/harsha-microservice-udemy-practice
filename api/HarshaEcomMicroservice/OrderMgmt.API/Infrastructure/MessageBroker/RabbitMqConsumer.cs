namespace OrderMgmt.API.Infrastructure.MessageBroker;

public class RabbitMqConsumer : IDisposable, IRabbitMqConsumer
{
    private readonly IConnection _connection;
    private readonly IConfiguration _config;
    private readonly ILogger<RabbitMqConsumer> _logger;

    public RabbitMqConsumer(
        IConfiguration config,
        ILogger<RabbitMqConsumer> logger)
    {
        _config = config;

        var connectoinFactory = new ConnectionFactory
        {
            HostName = _config["RabbitMQ_HostName"]!,
            Port = int.Parse(_config["RabbitMQ_Port"]!),
            UserName = _config["RabbitMQ_UserName"]!,
            Password = _config["RabbitMQ_Password"]!
        };

        _connection = connectoinFactory.CreateConnectionAsync().Result;
        _logger = logger;
    }

    public async Task StartConsumingAsync<T>(
        string queueName,
        string routingKey,
        Func<T, Task> onMessageReceived)
    {
        // create channel
        var channel = await _connection.CreateChannelAsync();

        // declare exchange
        string exchangeName = _config["RabbitMQ_ExchangeName"]!;

        await channel.ExchangeDeclareAsync(
            exchange: exchangeName,
            type: ExchangeType.Direct,
            durable: true,
            autoDelete: false
        );

        // declare queue
        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        // bind queue to exchange
        await channel.QueueBindAsync(
            queue: queueName,
            exchange: exchangeName,
            routingKey: routingKey
        );

        // set prefetch count (how many messages to fetch at once)
        await channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false
        );

        // handel consumer received event
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            try
            {
                var body = eventArgs.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<T>(json);

                if (message != null)
                {
                    await onMessageReceived(message);
                }

                // acknowledge the message
                await channel.BasicAckAsync(
                    deliveryTag: eventArgs.DeliveryTag,
                    multiple: false
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message: {ErrorMessage}", ex.Message);

                // Reject and requeue the message
                await channel.BasicNackAsync(
                    deliveryTag: eventArgs.DeliveryTag,
                    multiple: false,
                    requeue: true
                );
            }
        };

        // start consuming
        await channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer
        );
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
