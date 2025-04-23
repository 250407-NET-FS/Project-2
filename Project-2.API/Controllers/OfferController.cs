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

    // // Endpoint to retrieve all Offers
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Offer>>> GetAllOffers(){
    //     try
    //     {
    //         return Ok(await _offerService.GetAllAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }


}