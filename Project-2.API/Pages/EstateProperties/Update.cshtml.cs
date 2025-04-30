using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.EstateProperties {
    public class UpdateModel(
            ILogger<LayoutModel> logger,
            UserManager<User> userManager,
            PropertyController propertyController
            ): LayoutModel(logger, userManager) {
        private readonly PropertyController _controller = propertyController;

        public async Task<IActionResult> OnGet(Guid id) {
            //Property = await _controller.GetPropertyById(id);
            return Page();
        }

        [BindProperty]
        public Property? Property {get; set;}
        //public UpdatePropertyDTO PropertyInfo {get; set;}
        public new User? User {get; set;}
        //public FavoriteDto? BookmarkInfo {get; set;}
        public bool IsSaved {get; set;}
        public int DaysListed {get; set;}

        public async Task<IActionResult> OnUpdateAsync() {
            //await _controller.UpdateProperty(PropertyInfo);

            return RedirectToPage("api/property/id/{id}");
        }
    }
}