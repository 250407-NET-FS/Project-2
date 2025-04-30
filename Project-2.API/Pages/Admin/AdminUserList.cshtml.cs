using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.Admin {
    class UserModel(
        ILogger<LayoutModel> logger, UserManager<User> userManager, UserController controller
        ): LayoutModel(logger, userManager) {
        private readonly UserManager<User> _userManager = userManager;
        private readonly UserController _controller = controller;

        public async Task<IActionResult> OnGetAsync() {
            //UserList = await _controller.GetAllUsers();
            return Page();
        }

        [BindProperty]
        public List<User>? UserList {get; set;}

        // public async Task<IActionResult> OnDeactivate(Guid id) {
        //     if (!UserList.Find(u => u.Id.Equals(id)).isDeactivated) {
        //         UserList.Find(u => u.Id.Equals(id)).isDeactivated = true;
        //     }
        //     else {
        //         UserList.Find(u => u.Id.Equals(id)).isDeactivated = false;
        //     }
        // }


        // public async Task<IActionResult> OnBan(Guid id) {
        //     if (!UserList.Find(u => u.Id.Equals(id)).isBanned) {
        //         UserList.Find(u => u.Id.Equals(id)).isBanned = true;
        //     }
        //     else {
        //         UserList.Find(u => u.Id.Equals(id)).isBanned = false;
        //     }
        // }

        public async Task<IActionResult> OnDeleteAsync(Guid id) {
            // User user = _controller.DeleteUserById(id);
            // UserList!.Remove(user);
            return Page();
        }
    }
}