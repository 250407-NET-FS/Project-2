using Project_2.Models;

namespace Project_2.Data;

public interface IPropertyRepository : IBaseRepository<Property>
{
    public Task<IEnumerable<Property>> GetAllWithFilters(decimal priceMin, decimal priceMax, int numBedroom, decimal numBathroom);
    public void Update(UpdatePropertyDTO propertyInfo);

public interface IPropertyRepository : IBaseRepository<Property> {
    public Task<IEnumerable<Property>> GetAllWithFilters(
        string country,
        string state,
        string zip,
        string address,
        decimal priceMax,
        decimal priceMin,
        int numBedroom,
        decimal numBathroom);

    public void Update(Property propertyInfo);
}