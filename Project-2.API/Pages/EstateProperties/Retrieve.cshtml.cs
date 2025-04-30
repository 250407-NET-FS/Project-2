using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.EstateProperties {
    public class RetrieveModel(
            ILogger<LayoutModel> logger,
            UserManager<User> manager,
            PropertyController propertyController,
            FavoriteController favoriteController
            ): LayoutModel(logger, manager) {
        private readonly PropertyController _propertyController = propertyController;
        private readonly FavoriteController _favoriteController = favoriteController;
        private readonly ILogger<LayoutModel> _logger = logger;

        public async Task<IActionResult> OnGet(Guid id) {
            //Property = await _propertyController.GetPropertyById(id);
            //User = await User.getUserById(Property.OwnerID);
            DaysListed = DateTime.Now.Subtract(Property!.ListDate).Days;
            return Page();
        }

        [BindProperty]
        public Property? Property {get; set;}
        public new User? User {get; set;}
        //public FavoriteDto? BookmarkInfo {get; set;}
        public bool IsSaved {get; set;}
        public int DaysListed {get; set;}

        public async Task<IActionResult> BookmarkAsync() {
            if (IsSaved) {
                //await _favoriteController.RemoveFavorite();
                IsSaved = false;
            }
            else {
                //await _favoriteController.CreateFavorite(BookmarkInfo);
                IsSaved = true;
            }

            return RedirectToPage();
        }
    }
}