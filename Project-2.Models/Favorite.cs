using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project_2.Models;

[Table("Favorites")]
public class Favorite(Guid OwnerID, Guid PropertyID)
{
    [Key]
    public Guid FavoriteID { get; set; } = Guid.NewGuid();
    [Required]
    [ForeignKey("OwnerID")]
    public Guid OwnerID { get; set; } = OwnerID;    
    [Required]
    [ForeignKey("PropertyID")]
    public Guid PropertyID { get; set; } = PropertyID;

    public DateTime Date { get; set; } = DateTime.Now;
}
