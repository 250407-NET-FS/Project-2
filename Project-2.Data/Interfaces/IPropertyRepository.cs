using Project_2.Models;

namespace Project_2.Data;

public interface IPropertyRepository : IBaseRepository<Property>
{
    public Task<IEnumerable<Property>> GetAllWithFilters(decimal priceMin, decimal priceMax, int numBedroom, decimal numBathroom);
    public void Update(UpdatePropertyDTO propertyInfo);
}