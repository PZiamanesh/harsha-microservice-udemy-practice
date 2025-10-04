namespace UserMgmt.API.Infrastructure.DataAccess;

public class PostgresDbContext
{
    public IDbConnection Connection { get; set; }

    public PostgresDbContext(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("UserMgmtConnection")!;

        connectionString = connectionString
            .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST"))
            .Replace("$POSTGRES_PORT", Environment.GetEnvironmentVariable("POSTGRES_PORT"))
            .Replace("$POSTGRES_DB", Environment.GetEnvironmentVariable("POSTGRES_DB"))
            .Replace("$POSTGRES_USER", Environment.GetEnvironmentVariable("POSTGRES_USER"))
            .Replace("$POSTGRES_PASSWD", Environment.GetEnvironmentVariable("POSTGRES_PASSWD"));

        Connection = new NpgsqlConnection(connectionString);
    }
}
