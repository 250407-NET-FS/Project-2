using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.EstateProperties {
    public class RetrieveModel: PageModel {
        private readonly PropertyController _propertyController;
        private readonly UserController _userController;
        private readonly FavoriteController _favoriteController;

        public RetrieveModel(
            PropertyController propertyController,
            UserController userController,
            FavoriteController favoriteController
            ) {
            _userController = userController;
            _propertyController = propertyController;
            _favoriteController = favoriteController;
        }

        public async Task<IActionResult> OnGet(Guid id) {
            Property = await _propertyController.GetPropertyById(id);
            User = await _userController.GetUserById(Property.OwnerID);

            DaysListed = DateTime.Now.Subtract(Property!.ListDate).Days;
            return Page();
        }

        [BindProperty]
        public Property? Property {get; set;}
        public new User? User {get; set;}
        public FavoriteDto? BookmarkInfo {get; set;}
        public bool IsSaved {get; set;}
        public int DaysListed {get; set;}

        public async Task<IActionResult> BookmarkAsync() {
            if (IsSaved) {
                await _favoriteController.RemoveFavorite();
                IsSaved = false;
            }
            else {
                await _favoriteController.CreateFavorite(BookmarkInfo);
                IsSaved = true;
            }

            return RedirectToPage();
        }
    }
}