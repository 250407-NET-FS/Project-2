using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;

namespace Project_2.Services.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository, JazaContext context)
    {
        _propertyRepository = propertyRepository;

    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _propertyRepository.GetAllAsync();
    }

    public async Task<Property?> GetByIdAsync(Guid id)
    {
        return await _propertyRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Property property)
    {
        await _propertyRepository.AddAsync(property);
        await _propertyRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(PropertyUpdateDTO property)
    {
        _propertyRepository.Update(property);
        await _propertyRepository.SaveChangesAsync();
    }

    public async Task RemoveAsync(Property property)
    {
        _propertyRepository.Remove(property);
        await _propertyRepository.SaveChangesAsync();
    }
}