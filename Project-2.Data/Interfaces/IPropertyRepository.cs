using Project_2.Models;

namespace Project_2.Data;

public interface IPropertyRepository : IBaseRepository<Property> {
    public Task<IEnumerable<Property>> GetAllWithFilters(decimal priceMin, decimal priceMax, int numBedroom, double num);
    public void Update(Property propertyInfo);
}