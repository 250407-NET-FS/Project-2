using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_2.Models;
using Project_2.API;

namespace Project_2.Pages.Pages.Admin;

public class AdminIndexModel(
    ILogger<IndexModel> logger,
    UserController userController,
    PropertyController propertyController
    ) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;
    private readonly UserController _userController = userController;
    private readonly PropertyController _propertyController = propertyController;

    public User? Admin {get; set;}
}