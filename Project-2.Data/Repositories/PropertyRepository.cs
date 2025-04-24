using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class PropertyRepository : BaseRepository<Property>, IPropertyRepository {
    private readonly JazaContext _dbContext; 

    public PropertyRepository(JazaContext context) : base(context) {
        _dbContext = context;
    }

    public async Task<IEnumerable<Property>> GetAllWithFilters(decimal priceMin, decimal priceMax, int numBedroom, float numBathroom) {
        IQueryable<Property> query = _dbContext.Property.Where(p => 1 == 1);
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
            query = query.Where(p => p.Bathrooms >= (decimal)numBathroom);
        }
        return await query.ToListAsync();
    }

    /* Marks an entity as to-be-upserted until its update/insertion into the db
     * in the next SaveChanges() call. 
     * ENSURE that SaveChanges() is called after this, as it has no effect otherwise
     */
    public void Update(Property propertyInfo) {
        if (!_dbContext.Property.Any(p => p.PropertyID == propertyInfo.PropertyID)) {
            throw new Exception("No property found");
        }
        _dbContext.Property.Update(propertyInfo);
    }
}   