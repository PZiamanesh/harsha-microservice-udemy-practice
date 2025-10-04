namespace ProductMgmt.API;

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
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MySqlDbContext>());

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

        services.AddDbContext<MySqlDbContext>(ops =>
        {
            string connectionString = configuration.GetConnectionString("ProdMgmtConnection")!;

            connectionString = connectionString
                .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
                .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"))
                .Replace("$MYSQL_DB", Environment.GetEnvironmentVariable("MYSQL_DB"))
                .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER"))
                .Replace("$MYSQL_PASSWD", Environment.GetEnvironmentVariable("MYSQL_PASSWD"));

            ops.UseMySQL(connectionString);
        });

        return services;
    }
}
