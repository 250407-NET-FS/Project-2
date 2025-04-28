using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.Auth {
    public class LoginModel: PageModel {
        private readonly AuthController _controller;

        public LoginModel(AuthController controller) {
            _controller = controller;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        // PageModel has its own User object idependent of the model layer
        public LoginDto? UserInfo {get; set;}

        public async Task<IActionResult> OnLoginAsync() {
            //await _controller.Login();
            return RedirectToPage("./Index");
        }
    }
}