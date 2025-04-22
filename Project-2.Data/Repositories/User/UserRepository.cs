using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly Project_2DbContext _context;

    public UserRepository(Project_2DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsersAysnc()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid guid)
    {
        return await _context.Users.FindAsync(guid);
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> RemoveUserAsync(Guid guid)
    {
        var user = await _context.Users.FindAsync(guid);
        if (user is null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUserAysnc(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.UserID);
        if (existingUser is null) return false;

        // only update properties if values are not null
        // might need to add DTO to make cleaner
        if (!string.IsNullOrEmpty(user.FName))
            existingUser.FName = user.FName;

        if (!string.IsNullOrEmpty(user.LName))
            existingUser.LName = user.LName;

        if (!string.IsNullOrEmpty(user.Email))
            existingUser.Email = user.Email;

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            existingUser.PhoneNumber = user.PhoneNumber;

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            existingUser.PhoneNumber = user.PhoneNumber;

        if (user.IsAdmin == (true || false))
            existingUser.IsAdmin = user.IsAdmin;

        if (user.Status == (true || false))
            existingUser.Status = user.Status;

        await _context.SaveChangesAsync();
        return true;
    }


}