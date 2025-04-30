using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_2.Models;

namespace Project_2.Data;

public class PropertyRepository : BaseRepository<Property>, IPropertyRepository {
    private readonly JazaContext _dbContext; 

    public PropertyRepository(JazaContext context) : base(context) {
        _dbContext = context;
    }

    public async Task<IEnumerable<Property>> GetAllWithFilters(
        string country,
        string state,
        string zip,
        string address,
        decimal priceMax,
        decimal priceMin,
        int numBedroom,
        decimal numBathroom
    ) {
        IQueryable<Property> query = _dbContext.Property.Where(p => 1 == 1);
        if (!country.IsNullOrEmpty()) {
            query = query.Where(p => p.Country == country);
        }
        if (!state.IsNullOrEmpty()) {
            query = query.Where(p => p.State == state);
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
        return await query.ToListAsync();
    }

    /* Marks an entity as to-be-upserted until its update/insertion into the db
     * in the next SaveChanges() call. 
     * ENSURE that SaveChanges() is called after this, as it has no effect otherwise
     */
    public void Update(UpdatePropertyDTO propertyInfo) {
        if (!_dbContext.Property.Any(p => p.PropertyID == propertyInfo.PropertyID)) {
            throw new Exception("No property found");
        }
        Property property = _dbContext.Property.Where(p => p.PropertyID == propertyInfo.PropertyID);
        
        if(propertyInfo.Country is not null){
            property.Country = propertyInfo.Country;
        }
        if(propertyInfo.State is not null){
            property.State = propertyInfo.State;
        }
        if(propertyInfo.City is not null){
            property.City = propertyInfo.City;
        }
        if(propertyInfo.ZipCode is not null){
            property.ZipCode = propertyInfo.ZipCode;
        }
        if(propertyInfo.StreetAddress is not null){
            property.StreetAddress = propertyInfo.StreetAddress;
        }
        if(propertyInfo.StartingPrice is not null){
            property.StartingPrice = propertyInfo.StartingPrice;
        }
        if(propertyInfo.Bedrooms is not null){
            property.Bedrooms = propertyInfo.Bedrooms;
        }
        if(propertyInfo.Bathrooms is not null){
            property.Bathrooms = propertyInfo.Bathrooms;
        }
        if(propertyInfo.ListDate is not null){
            property.ListDate = propertyInfo.ListDate;
        }
        if(propertyInfo.OwnerID is not null){
            property.OwnerID = propertyInfo.OwnerID;
        }
        if(propertyInfo.StreetAddress is not null){
            property.StreetAddress = propertyInfo.StreetAddress;
        }
        if(propertyInfo.ForSale is not null){
            property.ForSale = propertyInfo.ForSale;
        }
        _dbContext.Property.Update(property);
    }
}   