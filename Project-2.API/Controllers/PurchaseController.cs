using Project_2.Models;
using Project_2.Services;

using Microsoft.AspNetCore.Mvc;

namespace Project_2.API;

// We need to designate this as an API Controller
// And we should probably set a top level route
// hint: If you use the [EntityName]Controller convention, we can essentially
// parameterize the route name
[ApiController]
[Route("api/purchase")]
public class PurchaseController : ControllerBase{

    // private readonly IPurchaseService _purchaseService;

    // public PurchaseController(IPurchaseService _purchaseService)
    // {
    //     _purchaseService = _purchaseService;
    // }

    // // Endpoint to retrieve all Purchases
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Purchase>>> GetAllPurchases(){
    //     try
    //     {
    //         return Ok(await _purchaseService.GetAllAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }


}