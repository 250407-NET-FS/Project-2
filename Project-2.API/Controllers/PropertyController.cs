using Project_2.Models;
using Project_2.Services.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Project_2.Models.DTOs;

namespace Project_2.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/property")]
public class PropertyController : ControllerBase{

    private readonly IPropertyService _propertyService;
    private readonly UserManager<User> _userManager;

    public PropertyController(IPropertyService propertyService, UserManager<User> userManager)
    {
        _propertyService = propertyService;
        _userManager = userManager;
    }

    // Get: api/property
    // Endpoint to retrieve all Properties
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetAllProperties(
        [FromQuery] string country = "",
        [FromQuery] string state = "",
        [FromQuery] string city = "",
        [FromQuery] string zip = "",
        [FromQuery] string address = "",
        [FromQuery] decimal minprice = -1,
        [FromQuery] decimal maxprice = -1,
        [FromQuery] int bedrooms = -1,
        [FromQuery] float bathrooms = -1,
        [FromQuery] bool forsale = false
        ){
        try
        {
            return Ok(await _propertyService.ShowAvailablePropertiesAsync(country, state, city, zip, address,
            minprice, maxprice, bedrooms, bathrooms, forsale));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Get: api/admin/property
    // Get all properties Admin Only
    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/admin/property")]
    public async Task<ActionResult<Property>> GetAllPropertiesAdmin(){
        try{
            return Ok(await _propertyService.ShowAvailablePropertiesAsync("", "",
            "", "", "", -1, -1, -1, -1, false));
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    //POST: api/property
    //Create a new property
    [Authorize]
    [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    public async Task<ActionResult<Property>> CreateProperty([FromBody] PropertyAddDTO dto)
    {
        try
        {
            //Explicitly checking the modelstate to make sure that out dto conforms
            //to whatever we need it to be
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _propertyService.AddNewPropertyAsync(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Put: api/property
    // Updates property attributes based on what is not null owner only
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<Property>> UpdateProperty([FromBody] PropertyUpdateDTO dto){
        try{
            User? user = await GetCurrentUserAsync();
            return Ok(await _propertyService.UpdatePropertyAsync(dto, user?.Id));
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    // Delete: api/property
    // Deletes property by property id owner only
    [Authorize]
    [HttpDelete("id/{id}")]
    public async Task<ActionResult<bool>> DeleteProperty([FromRoute] Guid id){
        try{
            User? user = await GetCurrentUserAsync();
            return Ok(await _propertyService.RemovePropertyAsync(id, user?.Id));
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    // Delete: api/admin/property/{id}
    // Deletes property by property id admin only
    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/admin/property/{id}")]
    public async Task<ActionResult<bool>> DeletePropertyAdmin([FromRoute] Guid id){
        try{
            return Ok(await _propertyService.DeletePropertyAdminAsync(id));
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    // Get: api/property/id/{id}
    // Get property by id
    [HttpGet("id/{id}")]
    public async Task<ActionResult<Property>> GetPropertyById([FromRoute] Guid id){
        try{
            return Ok(await _propertyService.GetByIdAsync(id));
        } catch (Exception e){
            return BadRequest(e.Message);
        }
    }

    private async Task<User?> GetCurrentUserAsync()
    {
        return await _userManager.GetUserAsync(HttpContext.User);
    }

}