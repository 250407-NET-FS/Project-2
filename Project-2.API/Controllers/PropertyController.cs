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

    // //POST: api/property
    // //Create a new property
    // [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    // public async Task<ActionResult<PropertyDto>> CreateOffer([FromBody] CreatePropertyDto dto)
    // {
    //     try
    //     {
    //         //Explicitly checking the modelstate to make sure that out dto conforms
    //         //to whatever we need it to be
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //         var created = await _propertyService.CreateAsync(dto);
    //         //If we pass model binding based on the rules we set via Data Annotations
    //         //inside of our CreatePropertyDto, and this object is created
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