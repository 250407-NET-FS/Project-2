using Project_2.Models;

namespace Project_2.Data.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAysnc();
    Task<User?> GetUserByIdAsync(Guid guid);
    Task AddUserAsync(User user);
    Task<bool> RemoveUserAsync(Guid guid);
}