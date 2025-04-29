using Microsoft.AspNetCore.Identity;

namespace Project_2.Models;

public class User : IdentityUser
{
    public string FullName { get; set; }
}