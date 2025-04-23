using Project_2.Models;
using Project_2.Services;

using Microsoft.AspNetCore.Mvc;

namespace Project_2.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase{

    //private readonly IUserService _userService;

    // public UserController(IUserService _userService)
    // {
    //     _userService = _userService;
    // }

    // // Get: api/user
    // // Endpoint to retrieve all Users
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<User>>> GetAllUsers(){
    //     try
    //     {
    //         return Ok(await _userService.GetAllAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }


}