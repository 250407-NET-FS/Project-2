using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Services.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public FavoriteService(IFavoriteRepository favoriteRepository, IPropertyRepository propertyRepository, IUserRepository userRepository)
    {
        _favoriteRepository = favoriteRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<FavoriteResponseDTO>> GetAllAsync()
    {
        IEnumerable<Favorite> favorites = await _favoriteRepository.GetAllAsync();
        return favorites.Select(f => new FavoriteResponseDTO
        {
            FavoriteId = f.FavoriteID,
            UserId = f.UserID,
            PropertyId = f.PropertyID,
            Date = f.Date
        });
    }

    public async Task<FavoriteResponseDTO?> GetByIdAsync(Guid id)
    {
        Favorite? favorite = await _favoriteRepository.GetByIdAsync(id);
        if (favorite is null) throw new Exception("Favorite not found");
        return new FavoriteResponseDTO
        {
            FavoriteId = favorite.FavoriteID,
            UserId = favorite.UserID,
            PropertyId = favorite.PropertyID,
            Date = favorite.Date
        };
    }

    public async Task<FavoriteResponseDTO> AddAsync(FavoritesAddDTO dto)
    {
        // check if property exists
        Property property = await _propertyRepository.GetByIdAsync(dto.PropertyId);
        if (property is null)
            throw new Exception("Property cannot be null");

        // check if user exists
        User user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user is null)
            throw new Exception("User cannot be null");

        // create new favorite using dto
        Favorite favorite = new Favorite(dto.PropertyId, dto.UserId);

        // add to database
        await _favoriteRepository.AddAsync(favorite);
        // save database
        await _favoriteRepository.SaveChangesAsync();

        return new FavoriteResponseDTO
        {
            FavoriteId = favorite.FavoriteID,
            UserId = favorite.UserID,
            PropertyId = favorite.PropertyID,
            Date = favorite.Date
        };
    }

    public async Task RemoveAsync(Guid favoriteId)
    {
        // Get favorite to be removed
        Favorite favorite = await _favoriteRepository.GetByIdAsync(favoriteId);

        // if favorite did not exist throw error
        if (favorite is null)
            throw new Exception("Favorite not found");

        // remove favorite from databse
        _favoriteRepository.Remove(favorite);
        await _favoriteRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<FavoriteResponseDTO>> GetAllForProperty(Guid propertyId)
    {
        // check if property exist
        Property property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property is null)
            throw new Exception("Property does not exist");

        // get list of favorites for property
        IEnumerable<Favorite> favorites = await _favoriteRepository.GetAllForProperty(propertyId);

        // return the list of property's favorites with dto
        return favorites.Select(f => new FavoriteResponseDTO
        {
            FavoriteId = f.FavoriteID,
            UserId = f.UserID,
            PropertyId = f.PropertyID,
            Date = f.Date
        });
    }

    public async Task<IEnumerable<FavoriteResponseDTO>> GetAllByUser(Guid userId)
    {
        // check if user exist
        User user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new Exception("User does not exist");

        // get list of favorites for user
        IEnumerable<Favorite> favorites = await _favoriteRepository.GetAllForProperty(userId);

        // return the list of user's favorites with dto
        return favorites.Select(f => new FavoriteResponseDTO
        {
            FavoriteId = f.FavoriteID,
            UserId = f.UserID,
            PropertyId = f.PropertyID,
            Date = f.Date
        });
    }
}