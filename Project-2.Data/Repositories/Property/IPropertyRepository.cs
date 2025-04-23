using Project_2.Models;

namespace Project_2.Data.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(Guid guid);
    Task AddAsync(Property property);
    void Update(Property property);
    void Remove(Property property);
    Task SaveChangesAsync();
}