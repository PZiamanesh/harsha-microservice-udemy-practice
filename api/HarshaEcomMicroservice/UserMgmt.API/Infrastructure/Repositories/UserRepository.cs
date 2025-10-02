namespace UserMgmt.API.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostgresDbContext _dbContext;

    public UserRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddUserAsync(User user)
    {
        const string query = @"
        INSERT INTO public.""Users"" (""UserID"", ""Email"", ""Password"", ""PersonName"", ""Gender"")
        VALUES (@UserID, @Email, @Password, @PersonName, @Gender)";

        var affected = await _dbContext.Connection.ExecuteAsync(query, user);
    }

    public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        var queryParams = new { Email = email, Password = password };

        const string query = @"
        select * from public.""Users"" 
        where ""Email""=@Email and ""Password""=@Password";

        var user = await _dbContext.Connection.QuerySingleOrDefaultAsync<User>(query, queryParams);

        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var queryParams = new { Email = email };

        const string query = @"
        select * from public.""Users"" 
        where ""Email""=@Email";

        var user = await _dbContext.Connection.QuerySingleOrDefaultAsync<User>(query, queryParams);

        return user;
    }
}
