using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API.Controllers;
using Project_2.Models;

namespace Project_2.Pages.Purchases {
    public class CreateModel: PageModel {
        private readonly PurchaseController _controller;

        public CreateModel(PurchaseController controller) {
            _controller = controller;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public Purchase? Purchase {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            await _controller.PostAsync();
            return RedirectToPage("./Index");
        }
    }
}