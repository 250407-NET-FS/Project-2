using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.API;
using Project_2.Models;

namespace Project_2.Pages.Pages.Admin {
    class UserModel(UserController controller): PageModel {
        private readonly UserController _controller = controller;

        public async Task<IActionResult> OnGetAsync() {
            //UserList = await _controller.GetAllUsers();
            return Page();
        }

        [BindProperty]
        public List<User>? UserList {get; set;}
        public int ListIndex {get; set;}

        public async Task<IActionResult> OnDeleteAsync(Guid id) {
            // User user = _controller.DeleteUserById(id);
            // UserList!.Remove(user);
            return Page();
        }
    }
}