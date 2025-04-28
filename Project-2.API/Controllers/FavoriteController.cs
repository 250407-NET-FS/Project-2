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
    //         return Ok(await _favoriteService.GetAllFavoritesAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // //POST: api/favorite
    // //Create a new favorite
    // [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    // public async Task<ActionResult<FavoriteDto>> CreateFavorite([FromBody] CreateFavoriteDto dto)
    // {
    //     try
    //     {
    //         //Explicitly checking the modelstate to make sure that out dto conforms
    //         //to whatever we need it to be
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //         var created = await _favoriteService.CreateFavoriteAsync(dto);
    //         //If we pass model binding based on the rules we set via Data Annotations
    //         //inside of our CreateFavoriteDto, and this object is created
    //         //We can not just echo back what the user sent in, but we can return
    //         //the actual object as it exists in our DB with its generated id and everything
    //         return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }


}