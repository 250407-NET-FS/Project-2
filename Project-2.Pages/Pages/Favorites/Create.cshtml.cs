using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API.Controllers;
using Project_2.Models;

namespace Project_2.Pages.Favorites {
    public class CreateModel: PageModel {
        private readonly FavoritesController _controller;

        public CreateModel(FavoritesController controller) {
            _controller = controller;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public Favorite? Favorite {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            await _controller.PostAsync();
            return RedirectToPage("./Index");
        }
    }
}