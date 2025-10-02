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
            string connString = configuration.GetConnectionString("ProductMgmtConnection")!;
            connString = connString.Replace("$HOST_NAME", Environment.GetEnvironmentVariable("HOST_NAME"))
                      .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));

            ops.UseMySQL(connString);
        });

        return services;
    }
}
