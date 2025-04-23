using Project_2.Models;

namespace Project_2.Data;

public interface IPropertyRepository : IBaseRepository<Property> {
    public Task<IEnumerable<Property>> GetAllWithFilters(decimal price);
    public void Update();
}