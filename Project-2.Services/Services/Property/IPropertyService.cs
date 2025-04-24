using Project_2.Models;

namespace Project_2.Services.Services;

public interface IPropertyService
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(Guid id);
    Task AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task RemoveAsync(Property property);
}