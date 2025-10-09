namespace ProductMgmt.API.Infrastructure.MessageBroker;

public class RabbitMqProducer: IRabbitMqProducer, IDisposable
{
    private readonly IConnection _connection;
    private readonly IConfiguration _config;
    private readonly ILogger<RabbitMqProducer> _logger;

    public RabbitMqProducer(
        IConfiguration config,
        ILogger<RabbitMqProducer> logger)
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

    public async Task PublishAsync<T>(string routingKey, T message)
    {
        string exchangeName = _config["RabbitMQ_ExchangeName"]!;

        using var channel = await _connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
                exchange: exchangeName,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false
                );

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

       await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: properties,
            body: body
        );

        _logger.LogInformation("Message published to RabbitMQ: {Message}", json);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
