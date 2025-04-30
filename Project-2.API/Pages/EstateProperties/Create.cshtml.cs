using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.EstateProperties {
    public class CreateModel(
        ILogger<LayoutModel> logger, UserManager<User> userManager,
        PropertyController controller
        ): LayoutModel(logger, userManager) {
        private readonly PropertyController _controller = controller;

        public IActionResult OnGet() {
            return Page();
        }

        //[BindProperty]
        //public CreatePropertyDto? PropertyInfo {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            //await _controller.CreateOffer(PropertyInfo);
            return RedirectToPage("./EstateProperties");
        }
    }
}