using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API.Controllers;
using Project_2.Models;

namespace Project_2.Pages.Users {
    public class RetrieveModel: PageModel {
        private readonly UserController _controller;

        public RetrieveModel(UserController controller) {
            _controller = controller;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        // PageModel has its own User object idependent of the model layer
        public new User? User {get; set;}

        // Note: To be used for admin section
        public async Task<IActionResult> OnGetAllAsync() {
            await _controller.GetAllAsync();
            return RedirectToPage("./Index");
        }
    }
}