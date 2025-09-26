namespace UserMgmt.API.Core.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);

    Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);

    Task<User?> GetUserByEmailAsync(string email);
}
