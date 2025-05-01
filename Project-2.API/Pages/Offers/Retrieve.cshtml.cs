using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Pages.Offers
{
    public class IndexModel : PageModel
    {
        private readonly OfferController _controller;
        private readonly UserManager<User> _userManager;

        public IndexModel(OfferController controller, UserManager<User> userManager)
        {
            _controller = controller;
            _userManager = userManager;
        }

        public List<Offer> Offers { get; set; } = new();
        public Guid PropertyId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid propertyId)
        {
            PropertyId = propertyId;

            var result = await _controller.GetAllOffersForProperty(propertyId);
            if (result?.Value != null)
            {
                Offers = result.Value.ToList();
            }

            return Page();
        }
    }
}