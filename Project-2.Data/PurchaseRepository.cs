using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly JazaContext _dbContext;

    public PurchaseRepository(JazaContext context) {
        _dbContext = context;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync() {
        return await _dbContext.Purchase.ToListAsync();
    }

    public async Task<Purchase?> GetByIdAsync(Guid guid) {
        return await _dbContext.Purchase.FindAsync(guid);
    }

    /* Marks an entity as to-be-inserted until its insertion into the db
     * in the next Save() call. 
     * ENSURE that Save() is called after this, as it has no effect otherwise
     */
    public async Task AddAsync(Purchase purchaseToAdd) {
        await _dbContext.Purchase.AddAsync(purchaseToAdd);
    }

    public async Task SaveChangesAsync() {
        await _dbContext.SaveChangesAsync();
    }


}
