using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Services;
using Project_2.Models;
using Microsoft.AspNetCore.Identity;

namespace Project_2.Pages.Pages.Auth {
    public class RegisterModel: LayoutModel {

        private readonly ILogger<RegisterModel> _logger;
        private readonly UserManager<User> _userManager;

        public RegisterModel(
            ILogger<RegisterModel> logger,
            ILogger<LayoutModel> layoutLogger,
            UserManager<User> userManager)        
            : base(layoutLogger, userManager)     
        {
            _logger = logger;
            
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