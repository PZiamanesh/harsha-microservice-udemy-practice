using ProductdMgmt.API.Infrastructure.Repositories;

namespace ProductdMgmt.API;

public static class ProgramDependencyRegister
{
    private static Assembly _assembly = typeof(ProgramDependencyRegister).Assembly;

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
            ops.UseMySQL(configuration.GetConnectionString("ProductMgmtConnection")!);
        });

        return services;
    }
}
