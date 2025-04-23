using Project_2.Models;

namespace Project_2.Data;

public interface IPurchaseRepository {
    public Task<IEnumerable<Purchase>> GetAllAsync();
    public Task<Purchase?> GetByIdAsync(Guid guid);
    public Task AddAsync(Purchase purchaseToAdd);
    public Task SaveChangesAsync();
}