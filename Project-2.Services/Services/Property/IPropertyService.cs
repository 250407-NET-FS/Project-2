using Project_2.Models;

namespace Project_2.Services.Services;

public interface IPropertyService
{
    Task<IEnumerable<Property>> ShowAvailablePropertiesAsync(
        string country,
        string state,
        string zip,
        string address,
        decimal minprice,
        decimal maxprice,
        int bedrooms,
        decimal bathrooms);
    Task<Property?> GetByIdAsync(Guid guid);
    Task AddNewPropertyAsync(Property property);
    Task MarkForSaleAsync(Guid propertyId);
    Task MarkSoldAsync(Guid propertyId, Guid newOwnerId);
    Task RemoveProperty(Guid propertyId, Guid currentUserId);
}