using Project_2.Models;

namespace Project_2.Data.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetById(Guid guid);
    Task<bool> AddUser(User user);
    Task<bool> RemoveUser(Guid guid);
    Task<bool> UpdateUser(User user);
}