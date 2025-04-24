using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class FavoritesGetDTO
{
    public Guid? FavoriteId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? PropertyId { get; set; }
}