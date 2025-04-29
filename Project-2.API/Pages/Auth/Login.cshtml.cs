using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Services;
using Project_2.Models;
using Microsoft.AspNetCore.Identity;

namespace Project_2.Pages.Pages.Auth {
    public class LoginModel: LayoutModel {
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<User> _userManager;

        public LoginModel(
            ILogger<LoginModel> logger,
            ILogger<LayoutModel> layoutLogger,
            UserManager<User> userManager)        
            : base(layoutLogger, userManager)     
        {
            _logger = logger;
            _userManager = userManager;
        }

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