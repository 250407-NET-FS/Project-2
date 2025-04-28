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

    // // Get: api/admin/purchase
    // // Endpoint to retrieve all Purchases
    // [Authorize(Roles = "Admin")]
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Purchase>>> GetAllPurchases(){
    //     try
    //     {
    //         return Ok(await _purchaseService.GetAllPurchasesAsync());
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // //POST: api/purchase
    // //Create a new purchase
    // [HttpPost] // In this method, we explicity tell ASP to look for our dto in the body of the request
    // public async Task<ActionResult<PurchaseDto>> CreateOffer([FromBody] CreatePurchaseDto dto)
    // {
    //     try
    //     {
    //         //Explicitly checking the modelstate to make sure that out dto conforms
    //         //to whatever we need it to be
    //         if (!ModelState.IsValid)
    //             return BadRequest(ModelState);
    //         var created = await _purchaseService.CreatePurchaseAsync(dto);
    //         //If we pass model binding based on the rules we set via Data Annotations
    //         //inside of our CreatePurchaseDto, and this object is created
    //         //We can not just echo back what the user sent in, but we can return
    //         //the actual object as it exists in our DB with its generated id and everything
    //         return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

    // // Get: api/purchase/user
    // // Get all purchases by user
    // public async Task<ActionResult<Purchase>> GetAllPurchasesByUser([FromBody] Guid id){
    //     try{
    //         return await Ok(_purchaseService.GetAllPurchasesByUserAsync(id));
    //     } catch(Exception e){
    //         return BadRequest(e.Message);
    //     }
    // }
}