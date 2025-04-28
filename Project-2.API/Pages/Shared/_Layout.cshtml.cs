using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.Models;
using Project_2.API;

namespace Project_2.Pages.Pages {
    public class LayoutModel(
    ILogger<IndexModel> logger//,
    //UserController userController
    ): PageModel {
        private readonly ILogger<IndexModel> _logger = logger;
        //private readonly UserController _userController = userController;

        public new User? User {get; set;}

        public bool IsLoggedIn() {
            return User is not null;
        }

        public IActionResult OnLogout()
        {
            User = null;
            return RedirectToPage("./Index");
        }
    }
}