namespace ProductdMgmt.API.Infrastructure.DataAccess;

public class MySqlDbContext : DbContext, IUnitOfWork
{
    public DbSet<Product> Products { get; set; }

    public MySqlDbContext(DbContextOptions<MySqlDbContext> ops) : base(ops)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(MySqlDbContext).Assembly);
        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
