using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Project_2.API;
using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Pages.Pages.EstateProperties {
    [BindProperties]
    public class UpdateModel(
            ILogger<LayoutModel> logger,
            UserManager<User> userManager,
            PropertyController propertyController,
            FavoritesDTO favoritesDTO,
            FavoriteController favoriteController
            ): LayoutModel(logger, userManager) {
        private readonly PropertyController _controller = propertyController;
        private readonly UserManager<User> manager = userManager;

        private readonly FavoriteController _favoriteController = favoriteController;

        public async Task<IActionResult> OnGet(Guid id) {
            Property = await GetProperty(id);
            User = await manager.FindByIdAsync(Property.OwnerID.ToString());
            DaysListed = DateTime.Now.Subtract(Property!.ListDate).Days;
            return Page();
        }

        public Property? Property {get; set;}
        public new User? User {get; set;}
        public FavoritesDTO? BookmarkInfo {get; set;}
        public bool IsSaved {get; set;}
        public int DaysListed {get; set;}

        public async Task<IActionResult> OnUpdateAsync(PropertyUpdateDTO dto) {
            await _controller.UpdateProperty(dto);
            Guid _id = Property.PropertyID;
            return RedirectToPage("./Retrieve", new { id = _id });
        }

        public async Task<IActionResult> BookmarkAsync(FavoritesDTO dto) {
            if (IsSaved) {
                await _favoriteController.MarkUnmarkFavorite(dto);
                IsSaved = false;
            }
            else {
                await _favoriteController.MarkUnmarkFavorite(dto);
                IsSaved = true;
            }

            return RedirectToPage();
        }

        public async Task<Property> GetProperty(Guid id)
        {
            var property = await _controller.GetPropertyById(id);
            return property.Value;
        }
    }
}