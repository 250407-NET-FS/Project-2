using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly JazaContext _context;

    public UserRepository(JazaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<User?> GetById(Guid guid)
    {
        return await _context.User.FindAsync(guid);
    }

    public async Task AddUser(User user)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.User.Add(user);
            await SaveUser();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error adding user", ex);
        }
    }

    public async Task<bool> RemoveUser(Guid guid)
    {
        var user = await _context.User.FindAsync(guid);
        if (user is null)
            throw new KeyNotFoundException($"User with ID {guid} does not exist");

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.User.Remove(user);
            await SaveUser();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error removing user", ex);
        }
    }

    public async Task<bool> UpdateUser(User user)
    {
        var existingUser = await _context.User.FindAsync(user.UserID);
        if (existingUser is null)
            throw new KeyNotFoundException($"User with ID {user.UserID} does not exist");

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
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

            await SaveUser();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error updating user", ex);
        }
    }

    private async Task SaveUser()
    {

        await _context.SaveChangesAsync();
    }
}