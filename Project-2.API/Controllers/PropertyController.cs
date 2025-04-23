using Project_2.Models;
using Project_2.Services;

using Microsoft.AspNetCore.Mvc;

namespace Project_2.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/property")]
public class PropertyController : ControllerBase{

    //private readonly IPropertyService _propertyService;

    // public PropertyController(IPropertyService _propertyService)
    // {
    //     _propertyService = _propertyService;
    // }

    // // Get: api/property
    // // Endpoint to retrieve all Properties
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Property>>> GetAllProperties(){
    //     try
    //     {
    //         return Ok(await _propertyService.GetAllAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }


}