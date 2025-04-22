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

    /* Marks an an entity as to-be-inserted until its insertion from the db
     * in the next Save() call. 
     * ENSURE that Save() is called after this, as it has no effect otherwise
     */
    public async Task AddFavoriteAsync(Favorite favToAdd) {
        await _dbContext.Favorite.AddAsync(favToAdd);
    }

    /* Marks an entity as to-be-deleted until its removal from the db
     * in the next Save() call. 
     * ENSURE that Save() is called after this, as it has no effect otherwise
     */
    public void Remove(Favorite favToRemove) {
        _dbContext.Favorite.Remove(favToRemove);
    }

    // Saves any insertions/deletions made to the db
    public async Task Save() {
        await _dbContext.SaveChangesAsync();
    }
}
