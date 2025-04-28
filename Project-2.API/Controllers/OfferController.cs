using Project_2.Models;
using Project_2.Services;

using Microsoft.AspNetCore.Mvc;

namespace Project_2.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/offer")]
public class OfferController : ControllerBase{

    // private readonly IOfferService _offerService;

    // public OfferController(IOfferService _offerService)
    // {
    //     _offerService = _offerService;
    // }

    // // Get: api/offer
    // // Endpoint to retrieve all Offers
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Offer>>> GetAllOffers(){
    //     try
    //     {
    //         return Ok(await _offerService.GetAllOffersAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // //POST: api/offer
    // //Create a new offer
    // [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    // public async Task<ActionResult<OfferDto>> CreateOffer([FromBody] CreateOfferDto dto)
    // {
    //     try
    //     {
    //         //Explicitly checking the modelstate to make sure that out dto conforms
    //         //to whatever we need it to be
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //         var created = await _offerService.CreateOfferAsync(dto);
    //         //If we pass model binding based on the rules we set via Data Annotations
    //         //inside of our CreateOfferDto, and this object is created
    //         //We can not just echo back what the user sent in, but we can return
    //         //the actual object as it exists in our DB with its generated id and everything
    //         return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/offer/id/{id}
    // // Get offer by id
    // [HttpGet]
    // public async Task<ActionResult<Offer>> GetOfferById([FromRoute] Guid id){
    //     try{
    //         return await Ok(_offerService.GetOfferByIdAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/offer/user/{id}
    // // Get all offer from user
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<User>>> GetAllOffersFromUser([FromRoute] Guid id){
    //     try{
    //         return await Ok(_offerService.GetAllOffersFromUserAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/offer/property/{id}
    // // Get all offers for property
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Property>>> GetAllOffersForProperty([FromRoute] Guid id){
    //     try{
    //         return await Ok(_offerService.GetAllOffersForPropertyAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }


}