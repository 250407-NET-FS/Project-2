using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class FavoriteRepository
{
    private readonly JazaContext _dbContext;

    public FavoriteRepository(JazaContext context) {
        _dbContext = context;
    }

    public async Task<IEnumerable<Favorite>> GetAllAsync() {
        return await _dbContext.Favorite.ToListAsync();
    }

    public async Task<Favorite?> GetByIdAsync(Guid guid) {
        return await _dbContext.Favorite.FindAsync(guid);
    }

    public async Task AddFavoriteAsync(Favorite favToAdd) {
        await _dbContext.Favorite.AddAsync(favToAdd);
    }

    public async Task Save() {
        await _dbContext.SaveChangesAsync();
    }
}
