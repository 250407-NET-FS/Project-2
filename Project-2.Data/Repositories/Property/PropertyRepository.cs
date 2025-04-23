using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly JazaContext _context;

    public PropertyRepository(JazaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllProperties()
    {
        return await _context.Property.ToListAsync();
    }

    public async Task<Property?> GetById(Guid guid)
    {
        return await _context.Property.FindAsync(guid);
    }

    public async Task<bool> AddProperty(Property property)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Property.Add(property);
            await SaveProperty();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error adding property", ex);
        }
    }

    public async Task<bool> RemoveProperty(Guid guid)
    {
        var property = await _context.Property.FindAsync(guid);

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Property.Remove(property);
            await SaveProperty();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error removing property", ex);
        }
    }

    public async Task<bool> UpdateProperty(Property property)
    {
        Property existingProperty = await _context.Property.FindAsync(property.PropertyID);

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // only update properties if values are not null
            // might need to add DTO to make cleaner
            if (property.StartingPrice != 0)
                existingProperty.StartingPrice = property.StartingPrice;

            if (property.OwnerID != Guid.Empty)
                existingProperty.OwnerID = property.OwnerID;


            await SaveProperty();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Error updating property", ex);
        }
    }

    private async Task SaveProperty()
    {

        await _context.SaveChangesAsync();
    }
}