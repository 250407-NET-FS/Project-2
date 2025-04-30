using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Data;

public class PropertyRepository : BaseRepository<Property>, IPropertyRepository {
    private readonly JazaContext _dbContext; 

    public PropertyRepository(JazaContext context) : base(context) {
        _dbContext = context;
    }

    public async Task<IEnumerable<Property>> GetAllWithFilters(
        string country,
        string state,
        string city,
        string zip,
        string address,
        decimal priceMax,
        decimal priceMin,
        int numBedroom,
        decimal numBathroom,
        bool forSale
    ) {
        IQueryable<Property> query = _dbContext.Property.Where(p => 1 == 1);
        if (!country.IsNullOrEmpty()) {
            query = query.Where(p => p.Country == country);
        }
        if (!state.IsNullOrEmpty()) {
            query = query.Where(p => p.State == state);
        }
        if (!city.IsNullOrEmpty()) {
            query = query.Where(p => p.City == city);
        }
        if (!zip.IsNullOrEmpty()) {
            query = query.Where(p => p.ZipCode == zip);
        }
        if (!address.IsNullOrEmpty()) {
            query = query.Where(p => p.StreetAddress == address);
        }
        if (priceMax > 0) {
            query = query.Where(p => p.StartingPrice <= priceMax);
        }
        if (priceMin >= 0 && priceMin <= priceMax) {
            query = query.Where(p => p.StartingPrice >= priceMin);
        }
        if (numBedroom != -1) {
            query = query.Where(p => p.Bedrooms >= numBedroom);
        }
        if (numBathroom != -1) {
            query = query.Where(p => p.Bathrooms >= numBathroom);
        }
        if (forSale) {
            query = query.Where(p => p.ForSale);
        }
        return await query.ToListAsync();
    }

    /* Marks an entity as to-be-upserted until its update/insertion into the db
     * in the next SaveChanges() call. 
     * ENSURE that SaveChanges() is called after this, as it has no effect otherwise
     */
    public void Update(PropertyUpdateDTO propertyInfo) {
        if (!_dbContext.Property.Any(p => p.PropertyID == propertyInfo.PropertyID)) {
            throw new Exception("No property found");
        }
        Property property = _dbContext.Property.Find(propertyInfo.PropertyID)!;
        
        property.Country = propertyInfo.Country ?? property.Country;
        property.State = propertyInfo.State ?? property.State;
        property.City = propertyInfo.City ?? property.City;
        property.ZipCode = propertyInfo.ZipCode ?? property.ZipCode;
        property.StreetAddress = propertyInfo.StreetAddress ?? property.StreetAddress;
        property.StartingPrice = propertyInfo.StartingPrice ?? property.StartingPrice;
        property.Bedrooms = propertyInfo.Bedrooms ?? property.Bedrooms;
        property.Bathrooms = propertyInfo.Bathrooms ?? property.Bathrooms;
        property.ListDate = propertyInfo.ListDate ?? property.ListDate;
        property.OwnerID = propertyInfo.OwnerID ?? property.OwnerID;
        property.StreetAddress = propertyInfo.StreetAddress;
        property.ForSale = propertyInfo.ForSale ?? property.ForSale;

        _dbContext.Property.Update(property);
    }
}   