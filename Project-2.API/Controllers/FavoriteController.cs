using Project_2.Models;
using Project_2.Services.Services;

using Microsoft.AspNetCore.Mvc;
using Project_2.Models.DTOs;

namespace Project_2.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/favorite")]
public class FavoriteController : ControllerBase{

    private readonly IFavoriteService _favoriteService;

    public FavoriteController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    // Get: api/favorite
    // Endpoint to retrieve all Favorites
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Favorite>>> GetAllFavorites(){
        try
        {
            return Ok(await _favoriteService.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //POST: api/favorite
    //Create a new favorite
    [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    public async Task<ActionResult<FavoritesAddDTO>> CreateFavorite([FromBody] FavoritesAddDTO dto)
    {
        try
        {
            //Explicitly checking the modelstate to make sure that out dto conforms
            //to whatever we need it to be
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _favoriteService.AddAsync(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}