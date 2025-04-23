using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API.Controllers;
using Project_2.Models;

namespace Project_2.Pages.Users {
    public class CreateModel: PageModel {
        private readonly UserController _controller;

        public CreateModel(UserController controller) {
            _controller = controller;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        // PageModel has its own User object idependent of the model layer
        public new User? User {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            await _controller.PostAsync();
            return RedirectToPage("./Index");
        }
    }
}