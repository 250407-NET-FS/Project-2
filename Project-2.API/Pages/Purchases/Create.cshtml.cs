using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Purchases {
    public class CreateModel(ILogger<LayoutModel> logger, UserManager<User> manager, PurchaseController controller): LayoutModel(logger, manager) {
        private readonly PurchaseController _controller;


        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public Purchase? Purchase {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            //await _controller.PostAsync();
            return RedirectToPage("./Index");
        }
    }
}