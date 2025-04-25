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

    public async Task<IEnumerable<Property>> ShowAvailablePropertiesAsync(
        string country,
        string state,
        string zip,
        string address,
        decimal minprice,
        decimal maxprice,
        int bedrooms,
        decimal bathrooms
    )
    {
        IEnumerable<Property> propertyList = await _propertyRepository.GetAllWithFilters(country, state, zip, address, minprice, maxprice, bedrooms, bathrooms);
        return propertyList.Where(p => p.ForSale);
    }

    public async Task<Property?> GetByIdAsync(Guid guid)
    {
        return await _propertyRepository.GetByIdAsync(guid);
    }

    public async Task AddNewPropertyAsync(Property property)
    {
        await _propertyRepository.AddAsync(property);
        int result = await _propertyRepository.SaveChangesAsync();
        if (result < 1) {
            throw new Exception("Failed to insert property");
        }
    }

    // requires updating if repo update method  is changed to use a property DTO
    // simply remove the get call and instead directly send the DTO
    public async Task MarkForSaleAsync(Guid propertyId) {
        Property? propertyToUpdate = await _propertyRepository.GetByIdAsync(propertyId);
        if (propertyToUpdate is null) {
            throw new Exception("Property not found");
        }

        propertyToUpdate.ForSale = true;
        propertyToUpdate.ListDate = DateTime.UtcNow;
        _propertyRepository.Update(propertyToUpdate);

        int result = await _propertyRepository.SaveChangesAsync();
        if (result < 1) {
            throw new Exception("Failed to update property");
        }
    }

    // requires updating if repo update method  is changed to use a property DTO
    // simply remove the get call and instead directly send the DTO
    public async Task MarkSoldAsync(Guid propertyId, Guid newOwnerId)
    {
        Property? propertyToUpdate = await _propertyRepository.GetByIdAsync(propertyId);
        if (propertyToUpdate is null) {
            throw new Exception("Property not found");
        }

        propertyToUpdate.ForSale = false;
        propertyToUpdate.OwnerID = newOwnerId;
        _propertyRepository.Update(propertyToUpdate);

        int result = await _propertyRepository.SaveChangesAsync();
        if (result < 1) {
            throw new Exception("Failed to update property");
        }
    }
    
    public async Task RemoveProperty(Guid propertyId, Guid userId)
    {
        Property? propertyToRemove = await _propertyRepository.GetByIdAsync(propertyId);
        if (propertyToRemove is null) {
            throw new Exception("Property not found");
        }

        if (propertyToRemove.OwnerID != userId) {
           throw new Exception("Unauthorized");
        }

        _propertyRepository.Remove(propertyToRemove);

        int result = await _propertyRepository.SaveChangesAsync();
        if (result < 1) {
            throw new Exception("Failed to update property");
        }
    }
}