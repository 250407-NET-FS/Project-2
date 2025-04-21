using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2.Models;

[Table("Users")]
public class User(string FName, string LName, string Email, string PhoneNumber)
{
    [Key]
    public Guid UserID { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(50)]
    public string? FName { get; set; } = FName;
    [Required]
    [StringLength(50)]
    public string? LName { get; set; } = LName;
    [Required]
    [EmailAddress]
    public string? Email { get; set; } = Email;
    [Phone]
    public string? PhoneNumber { get; set; } = PhoneNumber;

    public bool IsAdmin { get; set; } = false;

    public bool Status { get; set; } = false;
}
