using Project_2.Models;

namespace Project_2.Services.Services;

public interface IFavoriteService
{
    Task<IEnumerable<Favorite>> GetAllAsync();
    Task<Favorite?> GetByIdAsync(Guid id);
    Task AddAsync(Favorite favorite);
    Task RemoveAsync(Favorite favorite);
}