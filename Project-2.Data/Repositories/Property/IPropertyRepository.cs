using Project_2.Models;

namespace Project_2.Data.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllProperties();
    Task<Property?> GetById(Guid guid);
    Task<bool> AddProperty(Property property);
    Task<bool> RemoveProperty(Guid guid);
    Task<bool> UpdateProperty(Property property);
}