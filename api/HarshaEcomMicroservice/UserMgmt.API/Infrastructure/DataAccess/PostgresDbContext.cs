using Npgsql;
using System.Data;

namespace UserMgmt.API.Infrastructure.DataAccess;

public class PostgresDbContext
{
    public IDbConnection Connection { get; set; }

    public PostgresDbContext(IConfiguration configuration)
    {
        Connection = new NpgsqlConnection(configuration.GetConnectionString("UserMgmtConnection"));
    }
}
