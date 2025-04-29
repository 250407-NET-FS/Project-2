using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_2.Models;

[Table("Favorites")]
public class Favorite
{
    [Key]
    public Guid FavoriteID { get; set; } = Guid.NewGuid();
    [Required]
    [ForeignKey("UserID")]
    public string UserID { get; set; } 
    [Required]
    [ForeignKey("PropertyID")]
    public Guid PropertyID { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public Favorite(){}
}