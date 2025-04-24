using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class PropertyRepository : IPropertyRepository
{
    private readonly JazaContext _context;

    public PropertyRepository(JazaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _context.Property.ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(Guid guid)
    {
        return await _context.Property.FindAsync(guid);
    }

    public async Task AddAsync(Property property)
    {
        await _context.Property.AddAsync(property);
    }

    public void Update(Property property)
    {
        _context.Property.Update(property);
    }

    public void Remove(Property property)
    {
        _context.Property.Remove(property);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}