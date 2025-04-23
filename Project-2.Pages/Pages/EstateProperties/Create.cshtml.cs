using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API.Controllers;
using Project_2.Models;

namespace Project_2.Pages.EstateProperties {
    public class CreateModel: PageModel {
        private readonly PropertyController _controller;

        public CreateModel(PropertyController controller) {
            _controller = controller;
        }

        public IActionResult OnGet() {
            return Page();
        }

        [BindProperty]
        public Property? Property {get; set;}

        public async Task<IActionResult> OnPostAsync() {
            await _controller.PostAsync();
            return RedirectToPage("./Index");
        }
    }
}