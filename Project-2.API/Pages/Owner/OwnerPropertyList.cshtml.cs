using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.Owner {
    class PropertyModel(ILogger<LayoutModel> logger, UserManager<User> userManager, PropertyController propertyController, UserController userController): LayoutModel(logger, userManager) {
        private readonly ILogger<LayoutModel> _logger = logger;
        private readonly UserManager<User> _userManager = userManager;
        private readonly PropertyController _propertyController = propertyController;
        private readonly UserController _userController = userController;

        public async Task<IActionResult> OnGetAsync() {
            //PropertyList = await _controller.GetAllProperties();
            //Owner = _userManager.FindByLoginAsync();
            //PropertyList.FindAll(p => Owner.Id.Equals(p.OwnerID));
            return Page();
        }

        [BindProperty]
        public List<Property>? PropertyList {get; set;}
        public User? Owner {get; set;}

        public async Task<IActionResult> OnDeleteAsync(Guid id) {
            // Property property = _controller.DeletePropertyAdmin(id);
            // PropertyList!.Remove(property);
            return Page();
        }
    }
}