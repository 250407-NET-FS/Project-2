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

    public async Task<IEnumerable<Favorite>> GetAllFavoritesAsync()
    {
        IEnumerable<Favorite> favorites = await _favoriteRepository.GetAllAsync();
        return favorites;
    }

    public async Task MarkUnmarkFavoriteAsync(FavoritesDTO dto)
    {
        // check if property exists
        Property? property = await _propertyRepository.GetByIdAsync(dto.PropertyId);
        if (property is null)
            throw new Exception("Property does not exist");

        // check if user exists
        User? user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user is null)
            throw new Exception("User does not exist");

        // create new favorite using dto
        Favorite favorite = new Favorite(dto.PropertyId, dto.UserId);

        // add to database
        await _favoriteRepository.AddAsync(favorite);
        // save database
        await _favoriteRepository.SaveChangesAsync();
    }

    public async Task<bool> CheckFavoritedAsync(FavoritesDTO dto) {
        IEnumerable<Favorite> favs = await _favoriteRepository.GetAllByUser(dto.UserId);
        return favs.Any(f => f.PropertyID == dto.PropertyId);
    }

    public async Task<IEnumerable<FavoritesDTO>> GetAllForPropertyAsync(Guid propertyId)
    {
        // check if property exist
        Property? property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property is null)
            throw new Exception("Property does not exist");

        // get list of favorites for property
        IEnumerable<Favorite> favorites = await _favoriteRepository.GetAllForProperty(propertyId);

        // return the list of property's favorites with dto
        return favorites.Select(f => new FavoritesDTO
        {
            UserId = f.UserID,
            PropertyId = f.PropertyID,
        });
    }

    public async Task<IEnumerable<FavoriteListForUserDTO>> GetAllByUserAsync(Guid userId)
    {
        // check if user exist
        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new Exception("User does not exist");

        // get list of favorites for user
        IEnumerable<Favorite> favorites = await _favoriteRepository.GetAllByUser(userId);

        // return the list of user's favorites with dto
        return favorites.Select(f => new FavoriteListForUserDTO
        {
            PropertyId = f.PropertyID,
            Date = f.Date
        });
    }
}