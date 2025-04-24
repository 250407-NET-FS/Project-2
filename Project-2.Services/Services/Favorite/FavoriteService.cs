using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;

namespace Project_2.Services.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    public async Task<IEnumerable<Favorite>> GetAllAsync()
    {
        return await _favoriteRepository.GetAllAsync();
    }

    public async Task<Favorite?> GetByIdAsync(Guid id)
    {
        return await _favoriteRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Favorite favorite)
    {
        await _favoriteRepository.AddAsync(favorite);
        await _favoriteRepository.SaveChangesAsync();
    }

    public async Task RemoveAsync(Favorite favorite)
    {
        _favoriteRepository.Remove(favorite);
        await _favoriteRepository.SaveChangesAsync();
    }
}