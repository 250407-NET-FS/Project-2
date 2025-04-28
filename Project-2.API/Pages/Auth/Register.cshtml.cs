using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.Auth {
    public class RegisterModel: PageModel {
        private readonly AuthController _controller;

        public RegisterModel(AuthController controller) {
            _controller = controller;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public RegisterDto? UserInfo {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            //await _controller.Register(UserInfo);
            return RedirectToPage("./Login");
        }
    }
}