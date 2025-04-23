using Project_2.Models;
using Project_2.Services;

using Microsoft.AspNetCore.Mvc;

namespace Project_2.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/favorite")]
public class FavoriteController : ControllerBase{

    // private readonly IFavoriteService _favoriteService;

    // public FavoriteController(IFavoriteService _favoriteService)
    // {
    //     _favoriteService = _favoriteService;
    // }

    // // Get: api/favorite
    // // Endpoint to retrieve all Favorites
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Favorite>>> GetAllFavorites(){
    //     try
    //     {
    //         return Ok(await _favoriteService.GetAllAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }


}