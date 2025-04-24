using System.ComponentModel.DataAnnotations;

namespace Project_2.Models.DTOs;

public class OfferByUserDTO
{
    [Required]
    public int UserId { get; set; }
}