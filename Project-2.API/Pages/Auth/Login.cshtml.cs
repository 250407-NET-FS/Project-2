using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.Auth {
    public class LoginModel(AuthController controller) : PageModel {
        private readonly AuthController _controller = controller;

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        // PageModel has its own User object idependent of the model layer
        public LoginDto? UserInfo {get; set;}

        // public async Task<IActionResult> OnLoginAsync() {
        //     await _controller.Login(UserInfo);
        //     if (loggedUser.role == "Admin") {
        //         return RedirectToPage("./");
        //     }
        //     else {
        //         return RedirectToPage("./admin/home");
        //     }
        // }
    }
}