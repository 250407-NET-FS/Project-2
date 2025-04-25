using Project_2.Models;
using Project_2.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

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
    // public async Task<ActionResult<IEnumerable<Property>>> GetAllProperties(
    //     [FromQuery] string country = "",
    //     [FromQuery] string state = "",
    //     [FromQuery] string city = "",
    //     [FromQuery] string zip = "",
    //     [FromQuery] string address = "",
    //     [FromQuery] decimal minprice = -1,
    //     [FromQuery] decimal maxprice = -1,
    //     [FromQuery] int bedrooms = -1,
    //     [FromQuery] float bathrooms = -1,
    //     [FromQuery] bool forsale = false
    //     ){
    //     try
    //     {
    //         return Ok(await _propertyService.GetAllAsync(country, state, city, zip, address,
    //         minprice, maxprice, bedrooms, bathrooms, forsale));
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // //POST: api/property
    // //Create a new property
    // [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    // public async Task<ActionResult<Property>> CreateOffer([FromBody] CreatePropertyDto dto)
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

    // // Put: api/property
    // // Updates property attributes based on what is not null
    // [HttpPut]
    // public async Task<ActionResult<Property>> UpdatePropertyAsync([FromBody] UpdatePropertyDTO dto){
    //     try{
    //         return await Ok(_propertyService.UpdatePropertyAsync(dto));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/property/id/{id}
    // // Get property by id
    // [HttpGet]
    // [Route("api/property/id/{id}")]
    // public async Task<ActionResult<Property>> GetPropertyById([FromRoute] Guid id){
    //     try{
    //         return await OK(_propertyService.GetPropertyByIdAsync(id))
    //     } catch (Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/property/country/{country}
    // // Get properties by country
    // [HttpGet]
    // [Route("api/property/country/{country}")]
    // public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesByCountry([FromRoute] string country){
    //     try{
    //         return await Ok(_propertyService.GetPropertiesByCountryAsync(country));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/property/state/{state}
    // // Get properties by state
    // [HttpGet]
    // [Route("api/property/state{state}")]
    // public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesByState([FromRoute] string state){
    //     try{
    //         return await Ok(_propertyService.GetPropertiesByStateAsync(state));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/property/city/{city}
    // // Get properties by city
    // [HttpGet]
    // [Route("api/property/city/{city}")]
    // public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesByCity([FromRoute] string city){
    //     try{
    //         return await Ok(_propertySevice.GetPropertiesByCityAsync(city));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/property/zip/{zip}
    // // Get properties by zipcode
    // [HttpGet]
    // [Route("api/property/zip/{zip}")]
    // public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesByZip([FromRoute] string zip){
    //     try{
    //         return await Ok(_propertyService.GetPropertiesByZipAsync(zip));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/property/streetaddress/{address}
    // // Get properties by street address
    // [HttpGet]
    // [Route("api/property/streetaddress/{address}")]
    // public async Task<ActionResult<IEnumerable<Property>>> GetPropertiesByAddress([FromRoute] string address){
    //     try{
    //         return await Ok(_propertService.GetPropertiesByAddressAsync(address));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

}