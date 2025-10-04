using OrderMgmt.API.Infrastructure.HttpClientServices;

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

        AddMongoDbService(services, configuration);

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

        services.AddHttpClient<ProductClientService>(ops =>
        {
            ops.BaseAddress = new Uri($"http://{configuration["ProductMgmt_Host"]}:{configuration["ProductMgmt_Port"]}/api/products/");
        });

        services.AddHttpClient<UserClientService>(ops =>
        {
            ops.BaseAddress = new Uri($"http://{configuration["UserMgmt_Host"]}:{configuration["UserMgmt_Port"]}/api/users/");
        });

        return services;
    }

    private static void AddMongoDbService(
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
