using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;

namespace Project_2.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly JazaContext _context;

    public UserService(IUserRepository userRepository, JazaContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(User user)
    {
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task RemoveAsync(User user)
    {
        _userRepository.Remove(user);
        await _userRepository.SaveChangesAsync();
    }
}