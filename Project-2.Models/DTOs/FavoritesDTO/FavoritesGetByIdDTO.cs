using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class FavoritesGetByIdDTO
{
    [Required]
    public Guid PropertyId { get; set; }
    [Required]
    public Guid UserId { get; set; }

}