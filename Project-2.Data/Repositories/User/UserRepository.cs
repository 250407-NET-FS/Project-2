using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class UserRepository : IUserRepository
{
    private readonly JazaContext _context;

    public UserRepository(JazaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid guid)
    {
        return await _context.User.FindAsync(guid);
    }

    public async Task AddAsync(User user)
    {
        await _context.User.AddAsync(user);
    }

    public void Update(User user)
    {
        _context.User.Update(user);
    }

    public void Remove(User user)
    {
        _context.User.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}