using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Services.Services;

public interface IFavoriteService
{
    Task<IEnumerable<FavoriteResponseDTO>> GetAllAsync();
    Task<FavoriteResponseDTO?> GetByIdAsync(Guid id);
    Task<FavoriteResponseDTO> AddAsync(FavoritesAddDTO dto);
    Task RemoveAsync(Guid favoriteId);
    Task<IEnumerable<FavoriteResponseDTO>> GetAllForProperty(Guid propertyId);
    Task<IEnumerable<FavoriteResponseDTO>> GetAllByUser(Guid userId);
}