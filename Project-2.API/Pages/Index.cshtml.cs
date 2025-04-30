using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.Models;
using Project_2.API;
using Microsoft.AspNetCore.Identity;

namespace Project_2.Pages;

public class IndexModel(
    ILogger<IndexModel> logger,
    UserManager<User> manager//,
    // PropertyController propertyController
    ) : LayoutModel(logger, manager)
{
    private readonly ILogger<IndexModel> _logger = logger;
    // private readonly UserController _userController = userController;
    // private readonly PropertyController _propertyController = propertyController;

    public new User? User {get; set;}
    public required IList<Property> Properties {get; set;}
    public required ISet<string> States {get; set;}
    public int Offset {get; set;} = 0;
    public int Capacity {get; set;}
    
    // public IActionResult OnGet() {
    //     Properties = _propertyController.GetAllProperties();
    //     foreach (Property property in Properties) {
    //         States.Add(property.State!);
    //     }
    //     return Page();
    // }
    // public async Task OnGetUserAsync() {
    //     User = await _userController.GetAsync();
    // }

    // public async Task OnGetPropertyListAsync() {
    //     Properties = await _propertyController.GetAllAsync();
    // }

    public void OnIncrementPropertyOffset() {
        if (Offset < Properties.Count - 1) {
            Offset++;
        }
    }
    public void OnDecrementPropertyOffset() {
        if (Offset > 0) {
            Offset--;
        }
    }
}
