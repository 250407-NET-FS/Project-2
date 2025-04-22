using Project_2.Models;

namespace Project_2.Data;

public interface IFavoriteRepository {
    public Task<IEnumerable<Favorite>> GetAllAsync();
    public Task<Favorite?> GetByIdAsync(Guid guid);
    public Task AddAsync(Favorite favToAdd);
    public void Remove(Favorite favToRemove);
    public Task SaveChangesAsync();
}