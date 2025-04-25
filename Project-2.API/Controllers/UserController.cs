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

    // //POST: api/user
    // //Create a new user
    // [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    // public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
    // {
    //     try
    //     {
    //         //Explicitly checking the modelstate to make sure that out dto conforms
    //         //to whatever we need it to be
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //         var created = await _userService.CreateAsync(dto);
    //         //If we pass model binding based on the rules we set via Data Annotations
    //         //inside of our CreateUserDto, and this object is created
    //         //We can not just echo back what the user sent in, but we can return
    //         //the actual object as it exists in our DB with its generated id and everything
    //         return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/user/id/{id}
    // // Get user by id
    // [HttpGet]
    // public async Task<ActionResult<User>> GetUserById([FromRoute] Guid id){
    //     try{
    //         return await Ok(_userService.GetUserByIdAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

}