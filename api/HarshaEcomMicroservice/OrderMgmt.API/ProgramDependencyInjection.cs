using OrderMgmt.API.Infrastructure.BackgroundServices;
using OrderMgmt.API.Infrastructure.HttpClientServices;
using OrderMgmt.API.Infrastructure.MessageBroker;

namespace OrderMgmt.API;

public static class ProgramDependencyInjection
{
    private static Assembly _assembly = typeof(ProgramDependencyInjection).Assembly;

    public static IServiceCollection AddCoreLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(ops =>
        {
            ops.AddMaps(_assembly);
        });

        services.AddValidatorsFromAssembly(_assembly);

        return services;
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(ops =>
        {
            ops.RegisterServicesFromAssembly(_assembly);
            ops.AddOpenBehavior(typeof(ValidationPipeline<,>));
        });

        AddMongoDb(services, configuration);

        services.AddStackExchangeRedisCache(options =>
        {
            string redisConnection = configuration.GetConnectionString("Redis")!;
            redisConnection = redisConnection
                .Replace("$REDIS_HOST", Environment.GetEnvironmentVariable("REDIS_HOST"))
                .Replace("$REDIS_PORT", Environment.GetEnvironmentVariable("REDIS_PORT"));

            options.Configuration = redisConnection;
        });

        services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();

        return services;
    }

    public static IServiceCollection AddHttpClientServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        });

        services.AddSingleton<ResilienceService>();

        services.AddHttpClient<ProductClientService>(ops =>
        {
            ops.BaseAddress = new Uri($"http://{configuration["ProductMgmt_Host"]}:{configuration["ProductMgmt_Port"]}/api/products/");
        })
        .AddPolicyHandler((sp, request) =>
        {
            var resilienceService = sp.GetRequiredService<ResilienceService>();
            return resilienceService.GetCircuitBreakerPolicy();
        })
        .AddPolicyHandler((sp, request) =>
        {
            var resilienceService = sp.GetRequiredService<ResilienceService>();
            return resilienceService.GetRetryPolicy();
        });

        services.AddHttpClient<UserClientService>(ops =>
        {
            ops.BaseAddress = new Uri($"http://{configuration["UserMgmt_Host"]}:{configuration["UserMgmt_Port"]}/api/users/");
        });

        return services;
    }

    public static IServiceCollection AddBackgroundServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<ProductUpdateMesageHostedService>();

        return services;
    }

    private static void AddMongoDb(
        IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("MongoDB")!;

        connectionString = connectionString
          .Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGODB_HOST"))
          .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT"));

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        services.AddScoped<IMongoDatabase>(provider =>
        {
            IMongoClient client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE"));
        });
    }
}
