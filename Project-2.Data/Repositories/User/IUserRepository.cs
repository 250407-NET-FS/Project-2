using Project_2.Models;

namespace Project_2.Data.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid guid);
    Task AddAsync(User user);
    void Update(User user);
    void Remove(User user);
    Task SaveChangesAsync();
}