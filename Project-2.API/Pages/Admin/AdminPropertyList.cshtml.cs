using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.Admin {
    class PropertyModel(PropertyController propertyController, UserController userController): PageModel {
        private readonly PropertyController _propertyController = propertyController;
        private readonly UserController _userController = userController;

        public async Task<IActionResult> OnGetAsync() {
            //PropertyList = await _controller.GetAllProperties();
            // foreach (Property property in PropertyList) {
            //     User owner = _userController.GetUserById(property.OwnerID);
            //     OwnerList!.Add(owner);
            // }
            return Page();
        }

        [BindProperty]
        public List<Property>? PropertyList {get; set;}
        public List<User> OwnerList {get; set;} = [];

        public async Task<IActionResult> OnDeleteAsync(Guid id) {
            // Property property = _controller.DeletePropertyAdmin(id);
            // PropertyList!.Remove(property);
            return Page();
        }
    }
}