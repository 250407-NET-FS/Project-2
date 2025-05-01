using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Pages.Offers
{
    public class CreateModel : LayoutModel
    {
        private readonly OfferController _controller;
        private readonly UserManager<User> _userManager;

        public CreateModel(
            ILogger<LayoutModel> logger,
            UserManager<User> userManager,
            OfferController controller
        ) : base(logger, userManager)
        {
            _controller = controller;
            _userManager = userManager;
        }

        [BindProperty]
        public OfferNewDTO Offer { get; set; } = new();

        public IActionResult OnGet(Guid propertyId)
        {
            Offer.PropertyId = propertyId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToPage("../Auth/Login");

            Offer.UserId = user.Id;

            await _controller.CreateOffer(Offer);

            return RedirectToPage("./Index");
        }
    }
}