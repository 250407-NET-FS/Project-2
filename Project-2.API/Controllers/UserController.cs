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

    // // Get: api/admin/user
    // // Endpoint to retrieve all Users Admin Only
    // [Authorize(Roles = "Admin")]
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<User>>> GetAllUsers(){
    //     try
    //     {
    //         return Ok(await _userService.GetAllUsersAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Delete: api/admin/user/id/{id}
    // // Delete user by id Admin Only
    // [Authorize(Roles = "Admin")]
    // [HttpDelete]
    // public async Task<ActionResult<bool>> DeleteUserById([FromRoute] Guid id){
    //     try{
    //         return await Ok(_userService.DeleteUserByIdAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/admin/user/id/{id}
    // // Get user by id Admin Only
    // [Authorize(Roles = "Admin")]
    // [HttpGet]
    // public async Task<ActionResult<UserDTO>> GetUserByIdAdmin([FromRoute] Guid id){
    //     try{
    //         return await Ok(_userService.GetUserByIdAdminAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/user
    // // Get user by id
    // [HttpGet]
    // public async Task<ActionResult<UserDTO>> GetUserById([FromBody] Guid id){
    //     try{
    //         return await Ok(_userService.GetUserByIdAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

}